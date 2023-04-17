using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopAPI.Data;
using ShopAPI.Model;
using ShopAPI.ModelV;
using ShopAPI.Request;
using ShopAPI.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ShopAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Context db;
        private readonly ITaiKhoan taiKhoan;
        private readonly ApiSetting _setting;
        public UserController(Context _db, IOptionsMonitor<ApiSetting> options, ITaiKhoan tk)
        {
            db = _db;
            _setting = options.CurrentValue;
            taiKhoan = tk;
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost("Login")]
        public IActionResult Login(LoginRequest tk)
        {
            var user = db.TaiKhoans.SingleOrDefault(x => x.Email == tk.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(tk.Password, user.matKhau) && user.activer == true)
            {
                return Ok(new TrangThaiLogin
                {
                    Success = true,
                    Message = "Thanh Cong",
                    Data = GenerateToke(user)
                });
            }
            return Ok(new TrangThaiLogin
            {
                Success = false,
                Message = "Khong Thanh Cong",

            });
        }
        private string GenerateToke(TaiKhoan taiKhoan)
        {
            var jwt = new JwtSecurityTokenHandler();
            var bytes = Encoding.UTF8.GetBytes(_setting.SecretKey);
            var tokenDer = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, taiKhoan.Email),
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(bytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwt.CreateToken(tokenDer);
            return jwt.WriteToken(token);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            taiKhoan.Register(model);

            return Ok(new { message = "Registration successful" });
        }
        [Authorize]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                taiKhoan.Delete(id);
                return Ok(new { message = "Xóa Thành Công Tài Khoản Số" + "" + id });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [Authorize]
        [HttpPut("{id:int}")]
        public IActionResult Edit(TaiKhoanVM taiKhoanVM, int id)
        {
            if (id != taiKhoanVM.maTaiKhoan)
            {
                return BadRequest(new { message = "Cập Nhật Không Thành Công Tài Khoản:" + "" + taiKhoanVM.tenNguoiDung });
            }
            try
            {
                taiKhoan.Update(taiKhoanVM);
                return Ok(new { message = "Cập Nhật Người dùng " + "" + taiKhoanVM.tenNguoiDung + "" + "Thành Công" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public IActionResult GetAll() {

            try
            {
                return Ok(taiKhoan.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("ConfimEmail")]
        public IActionResult VerifyAccount(string id)
        {

            //  dc.Configuration.ValidateOnSaveEnabled = false;
            var v = db.TaiKhoans.Where(a => a.EmailActive == new Guid(id)).FirstOrDefault();
            if (v != null)
            {
                v.activer = true;
                db.SaveChanges();
            }
            else
            {
                return BadRequest();
            }
            return Ok(new { mess = "Thanh Cong" });
        }



    }
}

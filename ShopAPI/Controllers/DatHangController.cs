using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Model;
using ShopAPI.ModelV;
using ShopAPI.Services;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatHangController : ControllerBase
    {
        private readonly IDatHang datHang; 
        public DatHangController(IDatHang _datHang)
        {
            datHang = _datHang;
        }
     
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(datHang.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var check = datHang.GetById(id);
                if (check == null)
                {
                    return BadRequest(new { message = "Không Tìm Thấy Sản Phẩm Có Mã :" + id });
                }
                else
                {
                    return Ok(check);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [Authorize]
        [HttpPost]
        public IActionResult DatHang(DatHangVM vn) {
            try
            {
               var check= datHang.DatHang(vn);
                return Ok(check);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        
        }
        [Authorize]
        [HttpDelete("{id=int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                 datHang.Delete(id);
                return Ok(new {message="Xóa Thành Công"});

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}

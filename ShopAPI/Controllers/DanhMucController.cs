using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.ModelV;
using ShopAPI.Services;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhMucController : ControllerBase
    {
        private readonly IDanhMuc danhMuc;
        public DanhMucController(IDanhMuc _danhMuc)
        {
            this.danhMuc = _danhMuc;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(danhMuc.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{id:int}")]
        public IActionResult GetByid(int id)
        {
            try
            {
                return Ok(danhMuc.GetById(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                danhMuc.Delete(id);
                return Ok(new {message="Xóa Thành Công Danh Mục Có ID :" +id});
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize]
        [HttpPut("{id:int}")]
        public IActionResult Update(int id,DanhMucVM dm)
        {
            var check = danhMuc.GetById(id);
            if (check==null)
            {
                return BadRequest(new { message = "Không Tìm Thấy Danh Mục Cần Cập Nhật Mã Là:"+id });
            }
            else
            {
                try
                {
                    danhMuc.Update(dm);
                    return Ok(new {message="Đã Update Thành Công Danh Mục Có Mã :" +id});
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult Add(DanhMucVM dm)
        {
            try
            {
                var check = danhMuc.Add(dm);
                return Ok(check);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.ModelV;
using ShopAPI.Services;

namespace ShopAPI.Controllers
{
    [Route("api/SanPham")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPham sanPham;

       public SanPhamController(ISanPham _sanPham)
        {
            sanPham = _sanPham;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(sanPham.GetAll());   
            
            }catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{id:int}")]
        public IActionResult GetByID(int id)
        {
            try
            {
                var check = sanPham.GetById(id);
                if(check == null)
                {
                    return BadRequest(new { message = "Không Tìm Thấy Sản Phẩm Có Mã :"+id });
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
        [HttpPut("{id:int}")]
        public IActionResult Update(SanPhamVM asanPham , int id)
        {
            if (id != asanPham.maSanPham)
            {
                return BadRequest(new { message = "Không Tìm Thấy Sản Phẩm Có Mã :" + id });
            }
            try
            {
                 sanPham.Update(asanPham);
                return Ok(new { message = "Update Thành Công Sản Phẩm Có Mã :" + id });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                sanPham.Delete(id);
                return Ok(new { message = "Xóa Thành Công Sản Phẩm Có Mã :" + id });

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public IActionResult Add(SanPhamVM x)
        {
            try
            {
                var check = sanPham.Add(x);
                return Ok(check);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}

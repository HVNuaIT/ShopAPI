using System.ComponentModel.DataAnnotations;

namespace ShopAPI.Request
{
    public class RegisterRequest
    {
        [Required]
        public string tenNguoiDung { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string matKhau { get; set; }
        [Required]
        public string diaChi { get; set; }
        [Required]
        public string soDt { get;set; }
    }
}

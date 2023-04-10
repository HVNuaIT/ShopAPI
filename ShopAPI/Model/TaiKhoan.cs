using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAPI.Model
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        public int maTaiKhoan { get; set; }
        [Required(ErrorMessage="Xin Nhập Vào Tên Người Dùng")]
        [Display(Name ="Tên Người Dùng")]
        public string tenNguoiDung { get; set; }
        [Required(ErrorMessage = "Xin Nhập Vào Email")]       
        public string Email { get;set; }
        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Xin Nhập Vào Mật Khẩu")]
        [JsonIgnore]
        public string matKhau { get; set; }
        [Required(ErrorMessage = "Xin Nhập Số Điện Thoại")]
        public string soDT { get; set; }
        [Display(Name = "Địa Chỉ Nơi Ở")]
        [Required(ErrorMessage = "Xin Nhập Vào Địa Chỉ")]
        public string diaChi { get;set; }
        [Display(Name = "Quyền Quản Trị")]
        [Required(ErrorMessage = "Xin Chọn Vào Quyền Quản Trị")]
        public bool quyen { get; set; }
        [Display(Name = "Kích Hoạt Tài Khoản")]
        [Required(ErrorMessage = "Xin Chọn Active Tài Khoản")]
        public bool activer { get; set; }
        [Display(Name = "Chuổi Kích Hoạt Email")]
        [Required(ErrorMessage = "Xin Nhập Vào Chuỗi Active Email")]
        public Guid EmailActive { get; set; }
        
       

    }
}

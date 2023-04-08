using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAPI.Model
{
    [Table("DanhMuc")]
    public class DanhMuc
    {
        [Key]
        public int maDanhMuc { get; set; }
        [Display(Name = "Tên Danh Mục")]
        [Required(ErrorMessage = "Xin Nhập Vào Tên Danh Mục")]
        public string tenDanhMuc { get; set; }
    }
}

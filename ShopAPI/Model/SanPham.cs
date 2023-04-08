using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAPI.Model
{
    [Table("SanPham")]
    public class SanPham
    {
        [Key]
        public int maSanPham { get; set; }
        [Display(Name = "Tên Sản Phẩm")]
        [Required(ErrorMessage ="Xin Hay Nhap Ten San Pham")]
        public string tenSanPham { get; set; }
        [Display(Name = "Số Lượng")]
        [Required(ErrorMessage = "Xin Nhập Vào Số Lượng")]
        public int soLuong { get;set; }
        [Display(Name = "Gía")]
        [Required(ErrorMessage = "Xin Nhập Vào Giá")]
        public double? gia { get; set; }
        [Display(Name = "Mô Tả")]
        [Required(ErrorMessage = "Xin Nhập Vào Mô Tả")]
        public string moTa { get; set; }
        [Display(Name = "Hình Ảnh Của Sản Phẩm")]
        [Required(ErrorMessage = "Xin Nhập Vào Hình Ảnh")]
        public string hinhAnh { get; set; }
        [Display(Name = "Danh Mục ")]
        [Required(ErrorMessage = "Xin Nhập Vào Danh Muc")]
        public int? maLoai { get; set; }
        [ForeignKey("maLoai")]
        public DanhMuc danhMuc { get; set; }

    }
}

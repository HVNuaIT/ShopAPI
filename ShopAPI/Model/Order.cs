using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAPI.Model
{
    [Table("DonHang")]
    public class Order
    {
        [Key]
        public int maHangHoa { get;set; }
        [Required(ErrorMessage ="Không Để Trống")]   
        public int? maSanPham { get;set; }
        [ForeignKey("maSanPham")]
        public SanPham SanPham { get; set; }
        [Required(ErrorMessage = "Không Để Trống")]
        public int soLuong { get; set; }
        [Required(ErrorMessage = "Không Để Trống")]
        public double gia { get;set; }
        public  double thanhTien
        {
            get
            {
                return (soLuong * gia);
            }
        }
        [Required]
        public int maTaiKhoan { get; set; }
        [ForeignKey("maTaiKhoan")]
        public TaiKhoan TaiKhoan { get;set; }
        [Required]
        public string hinhAnh { get; set; }
        [Required]
        public string tenSanPham { set; get; }
    }
}

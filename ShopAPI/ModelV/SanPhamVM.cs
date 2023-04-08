using ShopAPI.Model;

namespace ShopAPI.ModelV
{
    public class SanPhamVM
    {
      
        public int maSanPham { get; set; }      
        public string tenSanPham { get; set; }       
        public int soLuong { get; set; }       
        public double? gia { get; set; }       
        public string moTa { get; set; }      
        public string hinhAnh { get; set; }      
        public int? maLoai { get; set; }
        public DanhMuc danhMuc { get; set; }
    }
}

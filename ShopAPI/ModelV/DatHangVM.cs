using ShopAPI.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopAPI.ModelV
{
    public class DatHangVM
    {
       
        public int maHangHoa { get; set; }
        
        public int? maSanPham { get; set; }

        public int soLuong { get; set; }
    
        public double gia { get; set; }

        public double thanhTien
        {
            get
            {
                return (soLuong * gia);
            }
        }
       public string tenSanPham { set; get; }
        public int maTaiKhoan { get; set; }
        public string hinhAnh { get; set; }
    }
}

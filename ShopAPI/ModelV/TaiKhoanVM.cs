using Newtonsoft.Json;

namespace ShopAPI.ModelV
{
    public class TaiKhoanVM
    {
      
        public int maTaiKhoan { get; set; }
      
        public string tenNguoiDung { get; set; }

        public string Email { get; set; }
     
        public string matKhau { get; set; }
       
        public string diaChi { get; set; }
       
        public bool quyen { get; set; }
        
        public bool activer { get; set; }
       
        public Guid EmailActive { get; set; }
    }
}

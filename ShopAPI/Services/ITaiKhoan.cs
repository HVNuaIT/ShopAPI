using ShopAPI.Model;
using ShopAPI.ModelV;
using ShopAPI.Request;

namespace ShopAPI.Services
{
    public interface ITaiKhoan
    {
        List<TaiKhoanVM> GetAll();
        TaiKhoanVM GetById(int id);
        TaiKhoanVM Add(TaiKhoanVM tk);
        void Update(TaiKhoanVM tk);
        void Delete(int id);
        RegisterRequest Register(RegisterRequest request);
      
    }
}

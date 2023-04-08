using ShopAPI.Model;
using ShopAPI.ModelV;

namespace ShopAPI.Services
{
    public interface ISanPham
    {
        List<SanPhamVM> GetAll();
        SanPhamVM GetById(int id);
        SanPhamVM Add(SanPhamVM sanPham);
        void Update(SanPhamVM sanPham);
        void Delete(int id);
    }
}

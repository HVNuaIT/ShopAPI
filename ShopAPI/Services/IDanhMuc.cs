using ShopAPI.Model;
using ShopAPI.ModelV;

namespace ShopAPI.Services
{
    public interface IDanhMuc
    {
        List<DanhMucVM> GetAll();
        DanhMucVM GetById(int id);
        DanhMucVM Add(DanhMucVM danhMuc);
        void Update(DanhMucVM danhMuc);
        void Delete(int id);

    }
}

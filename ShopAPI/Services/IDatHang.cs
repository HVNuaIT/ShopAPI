using ShopAPI.ModelV;

namespace ShopAPI.Services
{
    public interface IDatHang
    {
        List<DatHangVM> GetAll();
        void Update(int id);
        void Delete(int id);
        DatHangVM GetById(int id);
        DatHangVM DatHang(DatHangVM vm);
    }
}

using Microsoft.AspNetCore.Http.HttpResults;
using ShopAPI.Data;
using ShopAPI.Model;
using ShopAPI.ModelV;
using ShopAPI.Services;
using System.Linq;

namespace ShopAPI.Repository
{
    public class DatHangRepository : IDatHang
    {
        private readonly Context db;
        public DatHangRepository(Context _db)
        {
            db = _db;
        }

        public DatHangVM DatHang(DatHangVM vm)
        {
            var check = new Order
            {
                soLuong = vm.soLuong,
                gia = vm.gia,
                hinhAnh = vm.hinhAnh,
                maSanPham = vm.maSanPham,
                maTaiKhoan = vm.maTaiKhoan, 
                tenSanPham = vm.tenSanPham,
             
            };
            db.Orders.Add(check);
            db.SaveChanges();
            return new DatHangVM
            {
                soLuong = check.soLuong,
                gia = check.gia,
                hinhAnh = check.hinhAnh,
                maSanPham = check.maSanPham,
                maTaiKhoan = check.maTaiKhoan,
                tenSanPham = check.tenSanPham,
                maHangHoa = check.maHangHoa,
            };
        }

        public void Delete(int id)
        {
            var check = db.Orders.Where(x => x.maHangHoa == id).FirstOrDefault();
            if (check != null)
            {
                db.Remove(check);
                db.SaveChanges();
            }
        }

        public List<DatHangVM> GetAll()
        {
            var check = db.Orders.Select(x => new DatHangVM
            {
                maHangHoa=x.maHangHoa,
               tenSanPham=x.tenSanPham,
               gia=x.gia,
               hinhAnh=x.hinhAnh,
               maSanPham = x.maSanPham,
               maTaiKhoan=x.maTaiKhoan,
               soLuong = x.soLuong,
            });
            return check.ToList();
        }

        public DatHangVM GetById(int id)
        {
            var check = db.Orders.SingleOrDefault(x => x.maHangHoa == id);
            if (check != null)
            {
                return new DatHangVM
                {
                    tenSanPham = check.tenSanPham,
                    gia = check.gia,
                    hinhAnh = check.hinhAnh,
                    maSanPham = check.maSanPham,
                    maTaiKhoan = check.maTaiKhoan,
                    soLuong = check.soLuong,
                    maHangHoa=check.maHangHoa,
                    
                  
                };
            }
                return null;
        }

        public void Update(DatHangVM dh)
        {
            var check = db.Orders.SingleOrDefault(x => x.maHangHoa == dh.maHangHoa);
            check.tenSanPham = dh.tenSanPham;
            check.gia = dh.gia;
            check.hinhAnh = dh.hinhAnh;
            check.soLuong = dh.soLuong;
            check.maSanPham= dh.maSanPham;
            check.maTaiKhoan = dh.maTaiKhoan;
            
            db.SaveChanges();
        }

       
    }
}

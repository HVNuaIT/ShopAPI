using ShopAPI.Data;
using ShopAPI.Model;
using ShopAPI.ModelV;
using ShopAPI.Services;

namespace ShopAPI.Repository
{
    public class SanPhamRepository : ISanPham
    {
        private readonly Context db;
        public SanPhamRepository(Context _db) { 
        db= _db;
        }
        public void Update(SanPhamVM sanPham)
        {
            var check = db.SanPhams.SingleOrDefault(x => x.maSanPham == sanPham.maSanPham);
            check.tenSanPham = sanPham.tenSanPham;
            check.danhMuc= sanPham.danhMuc;
            check.moTa= sanPham.moTa;
            check.maLoai= sanPham.maLoai;
            check.gia= sanPham.gia;
            check.hinhAnh= sanPham.hinhAnh;
            check.soLuong= sanPham.soLuong;
            db.SaveChanges();
        }

        public List<SanPhamVM> GetAll()
        {
            var check = db.SanPhams.Select(o => new SanPhamVM
            {
                tenSanPham = o.tenSanPham,
                maLoai = o.maLoai,
                gia = o.gia,
                hinhAnh = o.hinhAnh,
                moTa = o.moTa,
                soLuong = o.soLuong,    
                maSanPham =o.maSanPham,
                danhMuc = o.danhMuc
            });
            return check.ToList();
        }

        public SanPhamVM GetById(int id)
        {
            var check = db.SanPhams.SingleOrDefault(x => x.maSanPham == id);
            if(check != null)
            {
                return new SanPhamVM
                {
                    tenSanPham = check.tenSanPham,
                    maLoai = check.maLoai,
                    gia = check.gia,
                    hinhAnh = check.hinhAnh,
                    maSanPham = check.maSanPham,
                    danhMuc = check.danhMuc,
                    moTa = check.moTa,
                    soLuong = check.soLuong
                };
            }
            return null ;
        }
        public SanPhamVM Add(SanPhamVM sanPham)
        {
            var check = new SanPham
            {
                soLuong = sanPham.soLuong,
                gia= sanPham.gia,
                hinhAnh=sanPham.hinhAnh,
                maLoai=sanPham.maLoai,
                moTa= sanPham.moTa,
                tenSanPham = sanPham.tenSanPham,
               
            };
            db.SanPhams.Add(check);
            db.SaveChanges();
            return new SanPhamVM { 
            maSanPham=check.maSanPham,
            tenSanPham=check.tenSanPham,
            maLoai=check.maLoai,
            danhMuc = check.danhMuc,
            gia = check.gia,
            hinhAnh = check.hinhAnh,
            moTa = check.moTa,
            soLuong = check.soLuong
            };
        }

        public void Delete(int id)
        {
            var check = db.SanPhams.SingleOrDefault(x => x.maSanPham == id);
            if(check != null)
            {
                db.Remove(check);
                db.SaveChanges();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ShopAPI.Data;
using ShopAPI.Model;
using ShopAPI.ModelV;
using ShopAPI.Services;
using System.Linq;

namespace ShopAPI.Repository
{
    public class DanhMucRepository : IDanhMuc
    {
        private readonly Context db;
        public DanhMucRepository(Context _db)
        {
            db = _db;
        }
        public void Update(DanhMucVM danhMuc)
        {
            var check = db.DanhMucs.SingleOrDefault(x => x.maDanhMuc == danhMuc.ID);

            check.tenDanhMuc = danhMuc.tenDanhMuc;
           db.Entry(check).State=EntityState.Modified;
            db.SaveChanges();
        }

        public List<DanhMucVM> GetAll()
        {
           
              var check = db.DanhMucs.Select(o => new DanhMucVM
            
              {
                 ID = o.maDanhMuc,
                  tenDanhMuc = o.tenDanhMuc
              });
            
            return check.ToList();
        }

        public DanhMucVM GetById(int id)
        {
            var check = db.DanhMucs.SingleOrDefault(x => x.maDanhMuc == id);
            if (check != null)
            {
                return new DanhMucVM
                {
                    ID = check.maDanhMuc,
                    tenDanhMuc = check.tenDanhMuc,
                 
                };
            }
            return null;
        }

        public DanhMucVM Add(DanhMucVM danhMuc)
        {
            var check = new DanhMuc
            {
               tenDanhMuc=danhMuc.tenDanhMuc
            };
            db.DanhMucs.Add(check);
            db.SaveChanges();
            return new DanhMucVM
            {
                  ID = check.maDanhMuc,
                tenDanhMuc = check.tenDanhMuc,
              
            };
        }
       
        public void Delete(int id)
        {
            var check = db.DanhMucs.SingleOrDefault(x => x.maDanhMuc == id);
            if (check != null)
            {
                db.Remove(check);
                db.SaveChanges();
            }
        }

       
    }
}

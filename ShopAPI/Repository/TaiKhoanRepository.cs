
using Azure.Core.Pipeline;
using Azure.Messaging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Data;
using ShopAPI.Model;
using ShopAPI.ModelV;
using ShopAPI.Request;
using ShopAPI.Services;

namespace ShopAPI.Repository
{
    public class TaiKhoanRepository : ITaiKhoan
    {

        private readonly Context db;
     
        public TaiKhoanRepository(Context _db)
        {
            db = _db;
        }
        public TaiKhoanVM Add(TaiKhoanVM taiKhoan)
        {

            var vm = new TaiKhoan()
            {
                tenNguoiDung = taiKhoan.tenNguoiDung,
                activer = true,
                EmailActive = Guid.NewGuid(),
                diaChi = taiKhoan.diaChi,
                Email = taiKhoan.Email,
                matKhau = BCrypt.Net.BCrypt.HashPassword(taiKhoan.matKhau),
                quyen = false,

            };
            db.TaiKhoans.Add(vm);
            db.SaveChanges();
            return new TaiKhoanVM
            {
                tenNguoiDung = vm.tenNguoiDung,
                activer = vm.activer,
                EmailActive = vm.EmailActive,
                diaChi = vm.diaChi,
                Email = vm.Email,
                matKhau = vm.matKhau,
                quyen = vm.quyen,

            };
        }
        public void Delete(int id)
        {
          var check = db.TaiKhoans.Where(x => x.maTaiKhoan == id).FirstOrDefault();
          if(check != null)
            {
                db.Remove(check);
                db.SaveChanges();
            }
        }
        public List<TaiKhoanVM> GetAll()
        {
          var check = db.TaiKhoans.Select(s=>new TaiKhoanVM
          {
              maTaiKhoan=s.maTaiKhoan,
              activer=s.activer,
              diaChi=s.diaChi,
              soDT=s.soDT,
              Email=s.Email,
              EmailActive=s.EmailActive,
              matKhau = s.matKhau,
              quyen = s.quyen,
              tenNguoiDung = s.tenNguoiDung
          });
            return check.ToList();
        }
        public TaiKhoanVM GetById(int id)
        {
            var check = db.TaiKhoans.Where(x => x.maTaiKhoan == id).FirstOrDefault();
            if (check != null)
            {
                return new TaiKhoanVM
                {
                    maTaiKhoan = check.maTaiKhoan,
                    activer = check.activer,
                    diaChi = check.diaChi,
                    soDT=check.soDT,
                    Email = check.Email,
                    EmailActive = check.EmailActive,
                    matKhau = check.matKhau,
                    quyen = check.quyen,
                    tenNguoiDung = check.tenNguoiDung 
                    
                };
            }
            return null ;
        }

        public RegisterRequest Register(RegisterRequest request)
        {
            var vm = new TaiKhoan()
            {
                tenNguoiDung = request.tenNguoiDung,
               // activer = true,
                //EmailActive = Guid.NewGuid(),
                diaChi = request.diaChi,
                Email = request.Email,
                matKhau = BCrypt.Net.BCrypt.HashPassword(request.matKhau),
                quyen = false,
                soDT=request.soDt,

            };
            db.TaiKhoans.Add(vm);
            db.SaveChanges();
            
            return new RegisterRequest
            {
                tenNguoiDung = vm.tenNguoiDung,
                // activer = true,
                //EmailActive = Guid.NewGuid(),
                diaChi = vm.diaChi,
                Email = vm.Email,
                matKhau = BCrypt.Net.BCrypt.HashPassword(vm.matKhau),
                 soDt=vm.soDT

            };
            
        }

        public void Update(TaiKhoanVM taiKhoans)
        {
            var check = db.TaiKhoans.SingleOrDefault(x => x.maTaiKhoan ==taiKhoans.maTaiKhoan);
            check.tenNguoiDung = taiKhoans.tenNguoiDung;
            check.diaChi = taiKhoans.diaChi;
            check.Email = taiKhoans.Email;
            check.quyen= taiKhoans.quyen;
            check.EmailActive = taiKhoans.EmailActive;
            check.soDT = taiKhoans.soDT;
            if (!string.IsNullOrEmpty(taiKhoans.matKhau))
                check.matKhau = BCrypt.Net.BCrypt.HashPassword(taiKhoans.matKhau);
            db.SaveChanges();
        }

       
    }
}

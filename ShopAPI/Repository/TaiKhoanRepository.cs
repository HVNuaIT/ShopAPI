
using Azure.Core.Pipeline;
using Azure.Messaging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Data;
using ShopAPI.Model;
using ShopAPI.ModelV;
using ShopAPI.Request;
using ShopAPI.Services;
using System.Net.Mail;
using System.Net;

namespace ShopAPI.Repository
{
    public class TaiKhoanRepository : ITaiKhoan
    {

        private readonly Context db;
     
        public TaiKhoanRepository(Context _db )
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
            if (check != null)
            {
                db.Remove(check);
                db.SaveChanges();
            }
        }
        public List<TaiKhoanVM> GetAll()
        {
            var check = db.TaiKhoans.Select(s => new TaiKhoanVM
            {
                maTaiKhoan = s.maTaiKhoan,
                activer = s.activer,
                diaChi = s.diaChi,
                soDT = s.soDT,
                Email = s.Email,
                EmailActive = s.EmailActive,
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
                    soDT = check.soDT,
                    Email = check.Email,
                    EmailActive = check.EmailActive,
                    matKhau = check.matKhau,
                    quyen = check.quyen,
                    tenNguoiDung = check.tenNguoiDung

                };
            }
            return null;
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
                soDT = request.soDt,
                activer = false,
                EmailActive = Guid.NewGuid(),
                
            };
            SendVerificationLinkEmail(vm.Email, vm.EmailActive.ToString());
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
                soDt = vm.soDT

            };

        }

        public void Update(TaiKhoanVM taiKhoans)
        {
            var check = db.TaiKhoans.SingleOrDefault(x => x.maTaiKhoan == taiKhoans.maTaiKhoan);
            check.tenNguoiDung = taiKhoans.tenNguoiDung;
            check.diaChi = taiKhoans.diaChi;
            check.Email = taiKhoans.Email;
            check.quyen = taiKhoans.quyen;
            check.EmailActive = taiKhoans.EmailActive;
            check.soDT = taiKhoans.soDT;
            if (!string.IsNullOrEmpty(taiKhoans.matKhau))
                check.matKhau = BCrypt.Net.BCrypt.HashPassword(taiKhoans.matKhau);
            db.SaveChanges();
        }


        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = EmailHelpSetting.Domen+ "api/Login/VerifyAccount/" + activationCode;

            var fromEmail = new MailAddress("phuongnama121999@gmail.com", "ShopVP");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "stimrgvviqxqgxyq";
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>Cảm ơn quý khách đã đăng kí tài khoản " +
                " Thành Công.Vui lòng click vào Link để thực hiện việc xác thực tài khoản và đăng nhập" +
                " <br/><br/><a href='" + verifyUrl + "'>" + verifyUrl + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

      

    }
}

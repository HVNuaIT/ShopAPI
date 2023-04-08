using Microsoft.EntityFrameworkCore;
using ShopAPI.Model;

namespace ShopAPI.Data
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<DanhMuc>DanhMucs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
    }
}

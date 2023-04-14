using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShopAPI.Data;
using ShopAPI.Model;
using ShopAPI.Repository;

using System.Text;
using AutoMapper;
using ShopAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(o=>o.AddDefaultPolicy(policy =>policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDanhMuc,DanhMucRepository>();
builder.Services.AddScoped<ISanPham, SanPhamRepository>();
builder.Services.AddScoped<ITaiKhoan, TaiKhoanRepository>();
builder.Services.AddScoped<IDatHang, DatHangRepository>();
builder.Services.Configure<ApiSetting>(builder.Configuration.GetSection("AppSetting"));
builder.Services.AddDbContext<Context>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
});

var secretKey = builder.Configuration["AppSetting:SecretKey"];
var setbyte =Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(otp =>

{
    otp.RequireHttpsMetadata = false;
    otp.SaveToken = true;
    otp.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(setbyte),
        ClockSkew = TimeSpan.Zero
    };
});
//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:"ShopAPI",
                      builder =>
                      {
                          builder.WithOrigins("http://example.com",
                                               "http://www.contoso.com") // Cấu Hình Kết Nối Với FE 
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("ShopAPI");
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

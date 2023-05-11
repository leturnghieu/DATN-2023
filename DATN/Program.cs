using DATN.Models;
using DATN.Services.Interfaces;
using DATN.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MasterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectString")));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
/*builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);*/
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<INguoiDung, NguoiDungService>();
builder.Services.AddScoped<IKhuVuc, KhuVucService>();
builder.Services.AddScoped<IKhoVatTu, KhoVatTuService>();
builder.Services.AddScoped<INhatKyMuaSam, NhatKyMuaSamService>();
builder.Services.AddScoped<INhatKySanXuat, NhatKySanXuatService>();
builder.Services.AddHostedService<ProductStatusService>();
builder.Services.AddScoped<INhatKyThuHoach, NhatKyThuHoachService>();
builder.Services.AddScoped<INhatKyBanSanPham, NhatKyBanSanPhamService>();
builder.Services.AddScoped<ICoSoNuoiTrong, CoSoNuoiTrongService>();
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auths}/{action=DangNhap}/{id?}");
app.Run();

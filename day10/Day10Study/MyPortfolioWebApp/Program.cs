using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPortfolioWebApp.Models;

namespace MyPortfolioWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // DB연결 초기화
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(
                builder.Configuration.GetConnectionString("SmartHomeConnection"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SmartHomeConnection"))
            ));

            // ASP.NET core Identity 설정
            // ASP.NET Core Identity를 사용하려면 아래 코드를 추가해야 함
            builder.Services.AddIdentity<CustomUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // --- 패스워드 정책---
            // 변경 전. 최대 6자리 이상, 특수문자 1개 포함, 영어 대소문자 포함
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // 아래와 같은 암호를 간단하게 설정하는 것은 보안상 좋지 않음!!!
                // 하지만, 개발 초기에는 편리하게 사용하기 위해 아래와 같이 설정할 수 있음
                options.Password.RequiredLength = 4; //최소길이 4자리
                options.Password.RequireNonAlphanumeric = false; //특수문자 사용 안함 
                options.Password.RequireUppercase = false; //영어 대문자 사용 안함
                options.Password.RequireLowercase = false; //영어 소문자 사용 안함
                options.Password.RequireDigit = false; //숫자 필수 여부

            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication(); // ASP.NET Core Identity 계정
            app.UseAuthorization(); // 권한

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

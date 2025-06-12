namespace WebFrontEndApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); // ASP.NET Core 시작 빌더객체 생성
            // 빌더 객체의 기능 : MVC 패턴 설정, DB 연결, 권한 설정, API 설정 등 웹앱의 전체 설정을 담당

            // MVC 패턴 설정 
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // 빌더로 만든 app 객체를 설정 
            app.UseStaticFiles(); // 정적 파일(HTML, CSS, JS 등) 제공 설정

            app.UseRouting(); // 라우팅 설정
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}

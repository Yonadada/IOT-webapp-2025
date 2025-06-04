using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolioWebApp.Models;
using System.Diagnostics;

namespace MyPortfolioWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context) // dB연동
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            // 정적 HTML을 Db 데이터로 동적처리
            //  DB에서 데이터를 불러온 뒤 About, Skill 객체에 데이터를 담아서 뷰로 전달
            var skillCount = _context.Skill.Count(); // 기술의 개수
            var skill = await _context.Skill.ToListAsync();

            //FirstOrDefaultAsync()는 데이터가 없으면 예외발생. FirstOrDefaultAsyn 데이터가 없으면 널값
            var about = await _context.About.FirstOrDefaultAsync(); // About 정보

            ViewBag.SkillCount = skillCount; // 뷰로 기술의 개수를 전달
            ViewBag.ColNum = (skillCount /2) + (skillCount % 2); // 3(7/2) + 1 (7%2)

            var model = new AboutModel();
            //model.About / 나중에
            model.About = about;
            model.Skill = skill; // 기술 정보를 모델에 담음
            return View(model);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolioWebApp.Models;
using System.Diagnostics;

namespace MyPortfolioWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context) // dB����
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            // ���� HTML�� Db �����ͷ� ����ó��
            //  DB���� �����͸� �ҷ��� �� About, Skill ��ü�� �����͸� ��Ƽ� ��� ����
            var skillCount = _context.Skill.Count(); // ����� ����
            var skill = await _context.Skill.ToListAsync();

            //FirstOrDefaultAsync()�� �����Ͱ� ������ ���ܹ߻�. FirstOrDefaultAsyn �����Ͱ� ������ �ΰ�
            var about = await _context.About.FirstOrDefaultAsync(); // About ����

            ViewBag.SkillCount = skillCount; // ��� ����� ������ ����
            ViewBag.ColNum = (skillCount /2) + (skillCount % 2); // 3(7/2) + 1 (7%2)

            var model = new AboutModel();
            //model.About / ���߿�
            model.About = about;
            model.Skill = skill; // ��� ������ �𵨿� ����
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

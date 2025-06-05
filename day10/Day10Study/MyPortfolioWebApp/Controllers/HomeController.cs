using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolioWebApp.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

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
            ViewBag.ColNum = (skillCount / 2) + (skillCount % 2); // 3(7/2) + 1 (7%2)

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


        [HttpPost]
        public async Task<IActionResult> Contact(ContactModel model)
        {
            if (ModelState.IsValid) //Model �� �� �װ��� ���� ����� �������� 
            {
                try
                {
                    var smtpClient = new SmtpClient("smtp.gmail.com") // Gmail�� ����Ϸ��� 
                    {
                        Port = 586, // ���� SMTP ������Ʈ ���� �ʿ�
                        Credentials = new NetworkCredential("hongwon5683@gmail.com", "��й�ȣ ����"),
                        EnableSsl = true // SSL ���
                                     
                    };
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("hongwon5683@gmail.com"),
                        Subject = model.Subject ?? "-[�������-]",
                        Body = $"������� : {model.Name} ({model.Email})\n\n �޽��� : {model.Message}",
                        IsBodyHtml = false, // ���� ������ HTML �±� ��� ����

                    };

                    mailMessage.To.Add("hongwon5683@gmail.com");  // �޴� ��� �̸��� �ּ�

                    await smtpClient.SendMailAsync(mailMessage); // �񵿱� ���� ����, �� ������ ���� ��ü�� ����!
                    ViewBag.Success = true; // ���� ���� ����
                }
                catch (Exception ex)
                {
                    ViewBag.Success = false;
                    ViewBag.Error = $"--���� ���� ����--, {ex.Message}";
                }
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

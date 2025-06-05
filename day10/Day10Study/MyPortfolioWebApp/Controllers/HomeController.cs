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
            ViewBag.ColNum = (skillCount / 2) + (skillCount % 2); // 3(7/2) + 1 (7%2)

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


        [HttpPost]
        public async Task<IActionResult> Contact(ContactModel model)
        {
            if (ModelState.IsValid) //Model 에 들어간 네개의 값이 제대로 들어왔으면 
            {
                try
                {
                    var smtpClient = new SmtpClient("smtp.gmail.com") // Gmail을 사용하려면 
                    {
                        Port = 586, // 메일 SMTP 서비스포트 변경 필요
                        Credentials = new NetworkCredential("hongwon5683@gmail.com", "비밀번호 노출"),
                        EnableSsl = true // SSL 사용
                                     
                    };
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("hongwon5683@gmail.com"),
                        Subject = model.Subject ?? "-[제목없음-]",
                        Body = $"보낸사람 : {model.Name} ({model.Email})\n\n 메시지 : {model.Message}",
                        IsBodyHtml = false, // 메일 본문에 HTML 태그 사용 여부

                    };

                    mailMessage.To.Add("hongwon5683@gmail.com");  // 받는 사람 이메일 주소

                    await smtpClient.SendMailAsync(mailMessage); // 비동기 메일 전송, 위 생성된 메일 객체를 전송!
                    ViewBag.Success = true; // 메일 전송 성공
                }
                catch (Exception ex)
                {
                    ViewBag.Success = false;
                    ViewBag.Error = $"--메일 전송 실패--, {ex.Message}";
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

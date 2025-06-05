using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPortfolioWebApp.Models;

namespace MyPortfolioWebApp.Controllers
{
    public class AccountController : Controller
    {
        //ASP.NET Core Identity에 필요한 변수
        private readonly UserManager<CustomUser> userManager;

        private readonly SignInManager<CustomUser> signInManager;

        // 생성자(alt + enter)
        public AccountController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager)
        {
            // UserManager나  singInManager에 null값이 들어오면 안된다. -> 아래코드로 예외처리
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        //News Controller Create(), POST Create()와 동일하게 생각
        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Register.cshtml 뷰를 반환
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {   
                // Id를 이메일로 사용하겠다
                var user = new CustomUser { 
                    UserName = model.Email,
                    Email = model.Email, 
                    City = model.City,
                    Mobile = model.Mobile,
                    Hobby = model.Hobby
                };

                var result = await userManager.CreateAsync(user, model.Password); // 자동으로 DB에 저장됨

                if (result.Succeeded)
                {
                    // 위의 저장한 유저로 로그인, isPersistent는 브라우저를 닫아도 로그인 상태를 유지할지 여부/ false 는 20~30분 동안 사용 안할시 로그아웃
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home"); // 회원가입 후(성공) 홈으로 이동
                }

                foreach ( var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
          
            return View(model); // 회원가입 (실패) 후 다시 회원가입 화면으로 이동
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(); // Login.cshtml 뷰를 반환
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if(result.Succeeded)
                {
                    return RedirectToAction(controllerName : "Home", actionName : "Index"); // 로그인 성공 후 홈으로 이동
                }
                    ModelState.AddModelError("", "로그인 실패");
                }
                return View(model);
            }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync(); // 로그아웃
            return RedirectToAction("Index", "Home"); // 로그아웃 후 홈으로 이동
        }
    }
}

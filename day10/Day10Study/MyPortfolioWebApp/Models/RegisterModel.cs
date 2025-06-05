using System.ComponentModel.DataAnnotations;

namespace MyPortfolioWebApp.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match.")]
        public string ConfirmPassword { get; set; }
        //추가로 PhgoneNumber 받으려면 string?(Nullable)로 선언해야함
        //public string? PhgoneNumber { get; set; } 


        public string? Mobile { get; set; } // 휴대폰 번호
        public string? City { get; set; } // 도시
        public string? Hobby { get; set; } // 취미




    }
}

using System.ComponentModel.DataAnnotations;

namespace MyPortfolioWebApp.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "이름은 필수 입력입니다.")]
        public string Name { get; set; } // 이름

        [Required(ErrorMessage = "이메일 형식에 맞춰서 입력해주세요.")]
        public string Email { get; set; } // 이메일

        [Required(ErrorMessage = "제목은 필수 입력입니다.")]
        public string Subject { get; set; } // 제목   

        [Required(ErrorMessage = "내용을 입력 해주세요.")]
        public string Message { get; set; } // 메시지
    }
}

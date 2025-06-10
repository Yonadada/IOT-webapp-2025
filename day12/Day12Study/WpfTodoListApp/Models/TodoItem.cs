using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; //DB 스키마 정의 클래스

namespace WpfTodoListApp.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }

        [Required] // Notnull일 경우는 string에 ? (nullable) 를 사용하지 않음!!
        [Column(TypeName ="VARCHAR(100)")]  // 이거 사용 하지 않으면 컬럼이 LONGTEXT로 생성됨~
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "CHAR(8)")] //20250610
        public string TodoDate { get; set; }

        //BOOLEAN 타입은 DB에 BIT로 저장됨
        public bool IsCompleted { get; set; }
    }
}

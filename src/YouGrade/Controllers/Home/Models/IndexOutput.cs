using YouGrade.Dtos;

namespace YouGrade.Controllers.Home.Models
{
    public class IndexOutput
    {
        public ExamDefDto Exam { get; set; }
        public string FullName { get; set; }
    }
}

namespace YouGrade.Dtos
{
    public class AnswerDto
    {
        public int ExamTakeId { get; set; }
        public int QuestionId { get; set; }
        public string AlternativeId { get; set; }
        public bool IsChecked { get; set; }
    }
}
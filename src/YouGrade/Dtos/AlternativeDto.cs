
namespace YouGrade.Dtos
{
    public class AlternativeDto
    {
        public string Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public bool Correct { get; set; }
        public bool IsChecked { get; set; }
     }
}
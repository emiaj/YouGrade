namespace YouGrade.Controllers.Home.Models
{
    public class SaveAnswerCommandInput
    {
        public int QuestionId { get; set; }
        public string Answers { get; set; }
    }
}
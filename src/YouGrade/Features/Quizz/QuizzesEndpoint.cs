namespace YouGrade.Features.Quizz
{
    public class QuizzesEndpoint
    {

    }


    public class QuizModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Questions { get; set; }
    }

}
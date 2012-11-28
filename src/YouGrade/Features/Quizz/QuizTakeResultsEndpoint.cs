using FubuMVC.Core;

namespace YouGrade.Features.Quizz
{
    public class QuizTakeResultsEndpoint
    {
        public QuizTakeResultsViewModel Get(QuizTakeResultsInputModel input)
        {
            return new QuizTakeResultsViewModel();
        }
    }

    public class QuizTakeResultsInputModel
    {
        public string TakeId { get; set; }
        [RouteInput]
        public int QuizId { get; set; }
    }

    public class QuizTakeResultsViewModel : QuizTakeResultsInputModel
    {
    }

}
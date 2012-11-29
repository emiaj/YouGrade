using FubuMVC.Core;
using YouGrade.Domain.Services;

namespace YouGrade.Features.Quizz
{
    public class QuizTakeResultsEndpoint
    {
        private readonly IQuizTakeService _quizTakeService;
        private readonly IQuizService _quizService;
        public QuizTakeResultsEndpoint(IQuizTakeService quizTakeService, IQuizService quizService)
        {
            _quizTakeService = quizTakeService;
            _quizService = quizService;
        }

        public QuizTakeResultsViewModel Get(QuizTakeResultsInputModel input)
        {
            var quiz = _quizService.GetById(input.QuizId);
            var take = _quizTakeService.Get(input.TakeId);

            return new QuizTakeResultsViewModel
                {
                    Title = quiz.Title,
                    Description = quiz.Description,
                    QuizId = input.QuizId,
                    TakeId = input.TakeId,
                    Thumbnail = quiz.Thumbnail,
                    PercentageCorrect = ((decimal)take.CorrectAnswers / take.Questions) * 100,
                    CorrectAnswers = take.CorrectAnswers,
                    IncorrectAnswers = take.IncorrectAnswers,
                    RetakeQuizModel = new QuizDetailInputModel { Id = input.QuizId }
                };
        }
    }

    public class QuizTakeResultsInputModel
    {
        [RouteInput]
        public string TakeId { get; set; }
        [RouteInput]
        public int QuizId { get; set; }
    }

    public class QuizTakeResultsViewModel : QuizTakeResultsInputModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public decimal PercentageCorrect { get; set; }
        public decimal PercentageIncorrect { get { return 100 - PercentageCorrect; } }
        public bool Approved { get { return PercentageCorrect > 50; } }
        public int CorrectAnswers { get; set; }
        public int IncorrectAnswers { get; set; }

        public QuizDetailInputModel RetakeQuizModel { get; set; }
    }

}
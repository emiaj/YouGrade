using FubuMVC.Core;
using FubuMVC.Core.Continuations;
using YouGrade.Domain.Services;
using YouGrade.Infrastructure;

namespace YouGrade.Features.Quizz
{
    public class QuizDetailEndpoint
    {
        private readonly IQuizService _service;
        private readonly IRandomStringGenerator _generator;

        public QuizDetailEndpoint(IQuizService service, IRandomStringGenerator generator)
        {
            _service = service;
            _generator = generator;
        }

        public QuizDetailViewModel Get(QuizDetailInputModel input)
        {
            var quiz = _service.GetById(input.Id);
            return new QuizDetailViewModel
                {
                    Description = quiz.Description,
                    Title = quiz.Title,
                    Id = quiz.Id,
                    Thumbnail = quiz.Thumbnail
                };
        }

        public FubuContinuation Post(QuizDetailViewModel input)
        {
            return FubuContinuation.RedirectTo(new QuizTakeInputModel
                {
                    Question = 1,
                    QuizId = input.Id,
                    TakeId = _generator.Generate()
                });
        }

    }

    public class QuizDetailInputModel
    {
        [RouteInput]
        public int Id { get; set; }
    }

    public class QuizDetailViewModel : QuizDetailInputModel
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
    }

}
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Continuations;
using YouGrade.Domain.Services;

namespace YouGrade.Features.Quizz
{
    public class QuizTakeEndpoint
    {
        private readonly IQuizService _quizService;

        public QuizTakeEndpoint(IQuizService quizService)
        {
            _quizService = quizService;
        }

        public QuizTakeViewModel Get(QuizTakeInputModel input)
        {
            var quiz = _quizService.GetById(input.QuizId);
            var question = quiz.Questions.First(x => x.QuestionNumber == input.Question);
            var alternatives = question.Alternatives.Select(x => new QuizTakeAlternative
                {
                    Number = x.AlternativeNumber, Text = x.AlternativeText
                });
            return new QuizTakeViewModel
                {
                    TakeId = input.TakeId,
                    QuizId = input.QuizId,
                    QuizDescription = quiz.Description,
                    QuizTitle = quiz.Title,
                    Question = input.Question,
                    QuestionText = question.QuestionText,
                    VideoPath = question.VideoPath,
                    HasPrevious = quiz.Questions.Any(x => x.QuestionNumber == input.Question - 1),
                    HasNext = quiz.Questions.Any(x => x.QuestionNumber == input.Question + 1),
                    Previous = new QuizTakeInputModel { Question = input.Question - 1, QuizId = input.QuizId, TakeId = input.TakeId },
                    Next = new QuizTakeInputModel { Question = input.Question + 1, QuizId = input.QuizId, TakeId = input.TakeId },
                    Alternatives = alternatives
                };
        }

        public FubuContinuation Post(QuizTakeViewModel input)
        {
            var quiz = _quizService.GetById(input.QuizId);
            if (quiz.Questions.Any(x => x.QuestionNumber == input.Question + 1))
            {
                return FubuContinuation.RedirectTo(new QuizTakeInputModel
                    {
                        Question = input.Question + 1,
                        QuizId = input.QuizId,
                        TakeId = input.TakeId
                    });
            }
            return FubuContinuation.RedirectTo(new QuizTakeResultsInputModel
                {
                    QuizId = input.QuizId,
                    TakeId = input.TakeId
                });
        }

    }

    public class QuizTakeInputModel
    {
        [RouteInput]
        public string TakeId { get; set; }
        [RouteInput]
        public int QuizId { get; set; }
        [RouteInput]
        public int Question { get; set; }

    }

    public class QuizTakeViewModel : QuizTakeInputModel
    {
        public string QuizTitle { get; set; }
        public string QuizDescription { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public QuizTakeInputModel Previous { get; set; }
        public QuizTakeInputModel Next { get; set; }
        public string QuestionText { get; set; }
        public string VideoPath { get; set; }

        public int SelectedAlternative { get; set; }
        public IEnumerable<QuizTakeAlternative> Alternatives { get; set; }

    }

    public class QuizTakeAlternative
    {
        public int Number { get; set; }
        public string Text { get; set; }
    }

}
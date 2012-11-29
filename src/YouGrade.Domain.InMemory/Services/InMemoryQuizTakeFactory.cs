using System.Collections.Generic;
using System.Linq;
using YouGrade.Domain.Services;

namespace YouGrade.Domain.InMemory.Services
{
    public class InMemoryQuizTakeFactory : IQuizTakeFactory
    {
        private readonly IQuizService _service;
        private readonly IList<QuizTake> _takes = new List<QuizTake>();

        public InMemoryQuizTakeFactory(IQuizService service)
        {
            _service = service;
        }

        public QuizTake GetOrCreate(string takeId, int quizId, string userId)
        {
            var take = get(takeId, quizId, userId);
            if (take == null)
            {
                lock (_takes)
                {
                    take = get(takeId, quizId, userId);
                    if (take == null)
                    {
                        var quiz = _service.GetById(quizId);
                        take = new QuizTake(takeId, quizId, userId, quiz.Questions.Count);
                        _takes.Add(take);
                    }
                }
            }
            return take;
        }

        private QuizTake get(string takeId, int quizId, string userId)
        {
            var take = _takes.FirstOrDefault(x => x.Id == takeId && x.QuizId == quizId && x.UserId == userId);
            return take;
        }

    }
}
using System.Collections.Generic;
using System.Linq;
using YouGrade.Domain.Services;

namespace YouGrade.Domain.InMemory.Services
{
    public class InMemoryQuizTakeService : IQuizTakeService
    {
        private readonly IList<QuizTake> _takes = new List<QuizTake>();

        public void Save(QuizTake quizTake)
        {
            lock (_takes)
            {
                _takes.Fill(quizTake);
            }
        }

        public QuizTake Get(string takeId)
        {
            lock (_takes)
            {
                return _takes.First(x => x.Id == takeId);
            }
        }
    }
}
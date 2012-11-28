using System;
using System.Collections.Generic;
using System.Linq;
using YouGrade.Domain.Services;

namespace YouGrade.Domain.InMemory.Services
{
    public class InMemoryQuizService : IQuizService
    {
        private readonly IList<Quiz> _quizzes = new List<Quiz>();

        public void Add(Quiz quiz)
        {
            _quizzes.Fill(quiz);
        }

        public IEnumerable<Quiz> GetByLanguage(string language)
        {
            return _quizzes.Where(x => x.Language.Equals(language, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
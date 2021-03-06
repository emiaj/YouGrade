using System.Collections.Generic;

namespace YouGrade.Domain.Services
{
    public interface IQuizService
    {
        IEnumerable<Quiz> GetByLanguage(string language);
        Quiz GetById(int id);
    }
}
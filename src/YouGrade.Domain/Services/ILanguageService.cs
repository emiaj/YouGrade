using System.Collections.Generic;

namespace YouGrade.Domain.Services
{
    public interface ILanguageService
    {
        IEnumerable<Language> GetAll();
    }

}
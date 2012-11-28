using System.Collections.Generic;
using YouGrade.Domain.Services;

namespace YouGrade.Domain.InMemory.Services
{
    public class InMemoryLanguageService : ILanguageService
    {
        private readonly IList<Language> _languages = new List<Language>();

        public void Add(Language language)
        {
            _languages.Fill(language);
        }

        public IEnumerable<Language> GetAll()
        {
            return _languages;
        }
    }
}
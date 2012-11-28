using System.Collections.Generic;
using Bottles;
using Bottles.Diagnostics;
using YouGrade.Domain.InMemory.Services;

namespace YouGrade.Domain.InMemory.Bootstrap
{
    public class LanguageDataFiller : IActivator
    {
        private readonly InMemoryLanguageService _service;

        public LanguageDataFiller(InMemoryLanguageService service)
        {
            _service = service;
        }

        public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
        {
            _service.Add(new Language("en", "English", "English"));
            _service.Add(new Language("es", "Español", "Spanish"));
        }
    }
}
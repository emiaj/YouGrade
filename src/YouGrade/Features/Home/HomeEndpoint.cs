using System.Collections.Generic;
using System.Linq;
using FubuCore;
using YouGrade.Domain.Services;
using YouGrade.Features.Quizz;

namespace YouGrade.Features.Home
{
    public class HomeEndpoint
    {
        private readonly ILanguageService _service;

        public HomeEndpoint(ILanguageService service)
        {
            _service = service;
        }

        public HomeViewModel Get(HomeInputModel input)
        {
            var languages = _service.GetAll()
                .Select(x => new LanguageHomeModel
                    {
                        Name = x.Id,
                        Title = "{0} - {1}".ToFormat(x.NeutralName, x.NativeName),
                        TargetModel = new ByLangInputModel {Id = x.Id},
                    });
            return new HomeViewModel { Languages = languages };
        }
    }

    public class HomeInputModel
    {
    }

    public class HomeViewModel
    {
        public IEnumerable<LanguageHomeModel> Languages { get; set; }
    }

    public class LanguageHomeModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public ByLangInputModel TargetModel { get; set; }
    }


}
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Urls;
using YouGrade.Domain.Services;

namespace YouGrade.Features.Quizz
{
    public class ByLangEndpoint
    {
        private readonly IQuizService _service;
        private readonly IUrlRegistry _registry;

        public ByLangEndpoint(IQuizService service, IUrlRegistry registry)
        {
            _service = service;
            _registry = registry;
        }

        public ByLangViewModel Get(ByLangInputModel input)
        {
            var quizzes = _service.GetByLanguage(input.Id).Select(x => 
                new QuizByLangModel
                {
                    Description = x.Description,
                    Thumbnail = x.Thumbnail,
                    Title = x.Title,
                    Url = _registry.UrlFor(new QuizDetailInputModel {Id = x.Id})
                });
            return new ByLangViewModel
                {
                    Quizzes = quizzes
                };
        }
    }

    public class ByLangInputModel
    {
        [RouteInput]
        public string Id { get; set; }
    }

    public class ByLangViewModel
    {
        public IEnumerable<QuizByLangModel> Quizzes { get; set; }
    }
    public class QuizByLangModel
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
    }

}
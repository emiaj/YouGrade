using Bottles;
using FubuMVC.Core;
using YouGrade.Domain.InMemory.Bootstrap;
using YouGrade.Domain.InMemory.Services;
using YouGrade.Domain.Services;

namespace YouGrade.Domain.InMemory
{
    public class InMemoryExtension : FubuPackageRegistry
    {
        public InMemoryExtension()
        {
            ILanguageService languageService = new InMemoryLanguageService();
            IQuizService quizService = new InMemoryQuizService();

            IQuizTakeFactory quizTakeFactory = new InMemoryQuizTakeFactory(quizService);
            IQuizTakeService quizTakeService = new InMemoryQuizTakeService();

            Services(x => x.SetServiceIfNone(languageService));
            Services(x => x.SetServiceIfNone((InMemoryLanguageService)languageService));
            Services(x => x.SetServiceIfNone(quizService));
            Services(x => x.SetServiceIfNone((InMemoryQuizService)quizService));

            Services(x => x.SetServiceIfNone(quizTakeFactory));
            Services(x => x.SetServiceIfNone(quizTakeService));

            Services(x => x.FillType<IActivator, LanguageDataFiller>());
            Services(x => x.FillType<IActivator, QuizDataFiller>());
        }
    }
}
using FubuCore;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Security;
using YouGrade.Attributes;
using YouGrade.Controllers.Home.Models;
using YouGrade.Services;

namespace YouGrade.Controllers.Home
{
    [AuthenticatedOnly]
    public class HomeController
    {
        private readonly IExamManager _examManager;
        private readonly IUserService _userService;
        private readonly ISecurityContext _securityContext;

        public HomeController(IExamManager examManager,IUserService userService,ISecurityContext securityContext)
        {
            _examManager = examManager;
            _userService = userService;
            _securityContext = securityContext;
        }

        public IndexOutput Index(IndexInput input)
        {
            var exam = _examManager.GetExam();
            var user = _userService.GetUser(_securityContext.CurrentIdentity.Name);
            return new IndexOutput {Exam = exam, FullName = "{0} {1}".ToFormat(user.Firstname, user.Lastname)};
        }

        public GetQuestionQueryOutput GetQuestionQuery(GetQuestionQueryInput input)
        {
            var question = _examManager.GetQuestion();
            return new GetQuestionQueryOutput { Question = question };
        }

        public SaveAnswerCommandOutput SaveAnswerCommand(SaveAnswerCommandInput input)
        {
            _examManager.SaveAnswer(input.QuestionId, input.Answers);
            return new SaveAnswerCommandOutput { QuestionId = input.QuestionId };
        }

        public FubuContinuation MoveToPreviousQuestionQuery(MoveToPreviousQuestionQueryInput input)
        {
            _examManager.MoveToPreviousQuestion();
            return FubuContinuation.RedirectTo<HomeController>(x => x.GetQuestionQuery(new GetQuestionQueryInput()));
        }

        public FubuContinuation MoveToNextQuestionQuery(MoveToNextQuestionQueryInput input)
        {
            _examManager.MoveToNextQuestion();
            return FubuContinuation.RedirectTo<HomeController>(x => x.GetQuestionQuery(new GetQuestionQueryInput()));
        }

        public EndExamQueryOutput EndExamQuery(EndExamQueryInput input)
        {
            var result = _examManager.EndExam();
            return new EndExamQueryOutput { Success = true, Result = result };
        }
    }
}

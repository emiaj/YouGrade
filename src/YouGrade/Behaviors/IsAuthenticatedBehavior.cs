using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Security;
using FubuMVC.Core.Urls;
using YouGrade.Controllers.Account.Models;
using YouGrade.Services;

namespace YouGrade.Behaviors
{
    public class IsAuthenticatedBehavior : BasicBehavior
    {
        private readonly IUrlRegistry _urls;
        private readonly IOutputWriter _writer;
        private readonly ISecurityContext _securityContext;
        private IExamTakeContext _examTakeContext;
        public IsAuthenticatedBehavior(IUrlRegistry urls, IOutputWriter writer, ISecurityContext securityContext, IExamTakeContext examTakeContext)
            : base(PartialBehavior.Executes)
        {
            _urls = urls;
            _examTakeContext = examTakeContext;
            _securityContext = securityContext;
            _writer = writer;
        }

        protected override DoNext performInvoke()
        {
            if(_securityContext.IsAuthenticated() && _examTakeContext.IsSet())
            {
                return DoNext.Continue;
            }
            var url = _urls.UrlFor(new LoginModel());
            _writer.RedirectToUrl(url);
            return DoNext.Stop;
        }
    }



}
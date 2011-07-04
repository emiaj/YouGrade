using AutoMapper;
using FubuMVC.Core.Continuations;
using YouGrade.Controllers.Account.Models;
using YouGrade.Controllers.Home;
using YouGrade.Controllers.Home.Models;
using YouGrade.Services;

namespace YouGrade.Controllers.Account
{
    public class AccountController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IExamTakeContext _examTakeContext;

        public AccountController(IAuthenticationService authenticationService, IUserService userService, IExamTakeContext examTakeContext)
        {
            _authenticationService = authenticationService;
            _examTakeContext = examTakeContext;
            _userService = userService;
        }

        public LoginModel LoginQuery(LoginModel model)
        {
            return model;
        }

        public FubuContinuation LoginCommand(LoginCommandModel input)
        {
            var valid = _authenticationService.Authenticate(input.UserName, input.Password);
            FubuContinuation continuation;
            if (valid)
            {
                var indexInput = new IndexInput();
                _examTakeContext.UserId = _userService.GetUser(input.UserName).Id;
                continuation = FubuContinuation.RedirectTo<HomeController>(x => x.Index(indexInput));
            }
            else
            {
                var loginModel = Mapper.Map<LoginCommandModel, LoginModel>(input);
                continuation = FubuContinuation.RedirectTo(loginModel);
            }
            return continuation;
        }

        public FubuContinuation LogOut(LogoutModel model)
        {
            _authenticationService.SignOut();
            return FubuContinuation.RedirectTo(new LoginModel());
        }
    }
}
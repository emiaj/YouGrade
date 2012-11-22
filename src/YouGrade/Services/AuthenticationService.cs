using FubuMVC.Core.Security;

namespace YouGrade.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationContext _authenticationContext;
        private readonly IUserService _userService;
        public AuthenticationService(IAuthenticationContext authenticationContext, IUserService userService)
        {
            _userService = userService;
            _authenticationContext = authenticationContext;
        }

        public bool Authenticate(string userName, string password)
        {
            if (_userService.UserExists(userName, password))
            {
                _authenticationContext.ThisUserHasBeenAuthenticated(userName, false);
                return true;
            }
            return false;
        }

        public void SignOut()
        {
            _authenticationContext.SignOut();
        }
    }
    public interface IAuthenticationService
    {
        bool Authenticate(string userName, string password);
        void SignOut();
    }
}
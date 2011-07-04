using System;
using System.Linq;
using YouGrade.Model;

namespace YouGrade.Services
{
    public class UserService : IUserService
    {
        private readonly YouGradeEntities _youGradeEntities;

        public UserService(YouGradeEntities youGradeEntities)
        {
            _youGradeEntities = youGradeEntities;
        }

        public User GetUser(string userName)
        {
            return _youGradeEntities.User.FirstOrDefault(x => x.Login == userName);
        }

        public bool UserExists(string userName, string password)
        {
            return _youGradeEntities.User
                .Where(x => x.Login == userName)
                .Where(x => x.Password == password)
                .Any();
        }
    }
    public interface IUserService
    {
        User GetUser(string userName);
        bool UserExists(string userName, string password);
    }

}
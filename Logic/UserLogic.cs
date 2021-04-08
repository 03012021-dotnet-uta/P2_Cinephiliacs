using System;
using GlobalModels;
using Repository;

namespace BusinessLogic
{
    public class UserLogic : Interfaces.IUserLogic
    {
        private readonly RepoLogic _repo;
        
        public UserLogic(RepoLogic repo)
        {
            _repo = repo;
        }

        public bool CreateUser(User user)
        {
            return _repo.AddUser(user);
        }

        public User GetUser(string username)
        {
            return _repo.GetUser(username);
        }
    }
}

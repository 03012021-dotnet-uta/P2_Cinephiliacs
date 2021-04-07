using System;
using GlobalModels;
using Repository;

namespace BusinessLogic
{
    public class UserMethods
    {
        private readonly TheRepo _repo;
        
        public UserMethods(TheRepo repo)
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

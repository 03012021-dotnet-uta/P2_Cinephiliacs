using System;
using System.Collections.Generic;
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
            var repoUser = UserMapper.UserToRepoUser(user);
            return _repo.AddUser(repoUser);
        }

        public User GetUser(string username)
        {
            var repoUser = _repo.GetUser(username);
            if(repoUser == null)
            {
                //_logger.LogWarning($"The repository layer's GetUser(${username}) returned null.");
                return null;
            }
            //_logger.LogWarning($"GetUser(${username}) WORKED, AND SO DID LOGGING!");
            return UserMapper.RepoUserToUser(repoUser);
        }

        public List<User> GetUsers()
        {
            var repoUsers = _repo.GetUsers();
            if(repoUsers == null)
            {
                //_logger.LogWarning($"The repository layer's GetUsers() returned null.");
                return null;
            }

            var users = new List<User>();
            foreach (var repoUser in repoUsers)
            {
                users.Add(UserMapper.RepoUserToUser(repoUser));
            }
            return users;
        }
    }
}

using System;
using System.Linq;
using Repository.Models;

namespace Repository
{
    public class RepoLogic
    {
        private readonly Cinephiliacs_DbContext _dbContext;

        public RepoLogic(Cinephiliacs_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddUser(GlobalModels.User user)
        {
            User repoUser = new User();
            repoUser.Username = user.Username;
            repoUser.FirstName = user.Firstname;
            repoUser.LastName = user.Lastname;
            repoUser.Email = user.Email;
            repoUser.Permissions = user.Permissions;

            _dbContext.Users.Add(repoUser);

            if(_dbContext.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public GlobalModels.User GetUser(string username)
        {
            User repoUser = _dbContext.Users.Where(a => a.Username == username).FirstOrDefault<User>();
            return new GlobalModels.User(repoUser.Username, repoUser.FirstName, repoUser.LastName, repoUser.Email, repoUser.Permissions);
        }
    }
}

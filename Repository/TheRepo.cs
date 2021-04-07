using System;
using System.Collections.Generic;
using System.Linq;
using Repository.DatabaseModels;

namespace Repository
{
    public class TheRepo
    {
        private readonly Cinephiliacs_DbContext _dbContext;
        public TheRepo(Cinephiliacs_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddUser(GlobalModels.User user)
        {
            User newUser = new User();
            newUser.Username = user.Username;
            newUser.FirstName = user.Firstname;
            newUser.LastName = user.Lastname;
            newUser.Email = user.Email;
            newUser.Permissions = user.Permissions;

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            return true;
        }

        public GlobalModels.User GetUser(string username)
        {
            User repoUser = _dbContext.Users.Where(a => a.Username == username).FirstOrDefault<User>();
            GlobalModels.User user = new GlobalModels.User(repoUser.Username, repoUser.FirstName, repoUser.LastName, repoUser.Email, repoUser.Permissions);
            return user;
        }
    }
}

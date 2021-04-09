using System;
using System.Collections.Generic;
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

        public bool AddUser(Repository.Models.User repoUser)
        {
            _dbContext.Users.Add(repoUser);

            if(_dbContext.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public Repository.Models.User GetUser(string username)
        {
            return _dbContext.Users.Where(a => a.Username == username).FirstOrDefault<User>();
        }

        public List<Repository.Models.User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }
    }
}

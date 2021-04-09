using System;
using System.Collections.Generic;
using GlobalModels;

namespace BusinessLogic
{
    public static class UserMapper
    {
        /// <summary>
        /// Maps an instance of GlobalModels.User onto Repository.Models.User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Repository.Models.User UserToRepoUser(User user)
        {
            var repoUser = new Repository.Models.User();
            repoUser.Username = user.Username;
            repoUser.FirstName = user.Firstname;
            repoUser.LastName = user.Lastname;
            repoUser.Email = user.Email;
            repoUser.Permissions = user.Permissions;

            return repoUser;
        }

        public static User RepoUserToUser(Repository.Models.User repoUser)
        {
            var user = new User(repoUser.Username, repoUser.FirstName, repoUser.LastName, repoUser.Email, repoUser.Permissions);
            return user;
        }

    }
}

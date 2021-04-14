using System.Collections.Generic;
using System.Threading.Tasks;
using GlobalModels;

namespace BusinessLogic.Interfaces
{
    public interface IUserLogic
    {
        /// <summary>
        /// Adds a new User Object to storage.
        /// Returns true if sucessful; false otherwise.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> CreateUser(User user);

        /// <summary>
        /// Returns the User object whose Username is equal to the username argument.
        /// Returns null if the username is not found.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUser(string username);

        /// <summary>
        /// Returns a list of every User object.
        /// </summary>
        /// <returns></returns>
        public Task<List<User>> GetUsers();

        /// <summary>
        /// Returns a list of every Discussion object that was created by the user
        /// specified by the username argument. Returns null if the username doesn't
        /// exist.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<List<Discussion>> GetDiscussions(string username);

        /// <summary>
        /// Returns a list of every Comment object that was created by the user
        /// specified by the username argument. Returns null if the username doesn't
        /// exist.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<List<Comment>> GetComments(string username);

        /// <summary>
        /// Returns a list of every FollowingMovie object that was created by the user
        /// specified by the username argument. Returns null if the username doesn't
        /// exist.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<List<string>> GetFollowingMovies(string username);

        /// <summary>
        /// Returns a list of every Review object that was created by the user
        /// specified by the username argument. Returns null if the username doesn't
        /// exist.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<List<Review>> GetReviews(string username);

        /// <summary>
        /// Adds a new FollowingMovie Object to storage.
        /// Returns true if sucessful; false otherwise.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="movieid"></param>
        /// <returns></returns>
        public Task<bool> FollowMovie(string username, string movieid);
    }
}
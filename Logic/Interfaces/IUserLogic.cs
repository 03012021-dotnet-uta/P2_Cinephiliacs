using System.Collections.Generic;
using GlobalModels;

namespace BusinessLogic.Interfaces
{
    public interface IUserLogic
    {
        public bool CreateUser(User user);
        public User GetUser(string username);
        List<User> GetUsers();
        List<Discussion> GetDiscussions(string username);
        List<Comment> GetComments(string username);
        List<string> GetFollowingMovies(string username);
        List<Review> GetReviews(string username);
        bool FollowMovie(string username, string movieid);
    }
}
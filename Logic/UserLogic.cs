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
            var repoUser = Mapper.UserToRepoUser(user);
            return _repo.AddUser(repoUser);
        }

        public User GetUser(string username)
        {
            var repoUser = _repo.GetUser(username);
            if(repoUser == null)
            {
                return null;
            }
            return Mapper.RepoUserToUser(repoUser);
        }

        public List<User> GetUsers()
        {
            var repoUsers = _repo.GetUsers();
            if(repoUsers == null)
            {
                return null;
            }

            var users = new List<User>();
            foreach (var repoUser in repoUsers)
            {
                users.Add(Mapper.RepoUserToUser(repoUser));
            }
            return users;
        }

        public List<Discussion> GetDiscussions(string username)
        {
            List<Repository.Models.Discussion> repoDiscussions = _repo.GetUserDiscussions(username);
            if(repoDiscussions == null)
            {
                return null;
            }

            List<Discussion> discussions = new List<Discussion>();
            foreach (var repoDiscussion in repoDiscussions)
            {
                // Get the topic associated with this discussion
                Repository.Models.Topic topic = _repo.GetDiscussionTopic(repoDiscussion.DiscussionId);
                discussions.Add(Mapper.RepoDiscussionToDiscussion(repoDiscussion, topic));
            }
            return discussions;
        }

        public List<Comment> GetComments(string username)
        {
            List<Repository.Models.Comment> repoComments = _repo.GetUserComments(username);
            if(repoComments == null)
            {
                return null;
            }

            List<Comment> comments = new List<Comment>();
            foreach (var repoComment in repoComments)
            {
                comments.Add(Mapper.RepoCommentToComment(repoComment));
            }
            return comments;
        }

        public List<string> GetFollowingMovies(string username)
        {
            List<Repository.Models.FollowingMovie> repoFollowingMovies = _repo.GetFollowingMovies(username);
            if(repoFollowingMovies == null)
            {
                return null;
            }

            List<string> followingMovies = new List<string>();
            foreach (var repoFollowingMovie in repoFollowingMovies)
            {
                followingMovies.Add(repoFollowingMovie.MovieId);
            }
            return followingMovies;
        }

        public List<Review> GetReviews(string username)
        {
            List<Repository.Models.Review> repoReviews = _repo.GetUserReviews(username);
            if(repoReviews == null)
            {
                return null;
            }

            List<Review> reviews = new List<Review>();
            foreach (var repoReview in repoReviews)
            {
                reviews.Add(Mapper.RepoReviewToReview(repoReview));
            }
            return reviews;
        }

        public bool FollowMovie(string username, string movieid)
        {
            Repository.Models.FollowingMovie repoFollowingMovie = new Repository.Models.FollowingMovie();
            repoFollowingMovie.Username = username;
            repoFollowingMovie.MovieId = movieid;
            
            return _repo.FollowMovie(repoFollowingMovie);
        }
    }
}

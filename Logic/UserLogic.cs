using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<bool> CreateUser(User user)
        {
            var repoUser = Mapper.UserToRepoUser(user);
            return await _repo.AddUser(repoUser);
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

        public async Task<List<User>> GetUsers()
        {
            var repoUsers = await _repo.GetUsers();
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

        public async Task<List<Discussion>> GetDiscussions(string username)
        {
            List<Repository.Models.Discussion> repoDiscussions = await _repo.GetUserDiscussions(username);
            if(repoDiscussions == null)
            {
                return null;
            }

            List<Discussion> discussions = new List<Discussion>();
            foreach (var repoDiscussion in repoDiscussions)
            {
                // Get the topic associated with this discussion
                Repository.Models.Topic topic = _repo.GetDiscussionTopic(repoDiscussion.DiscussionId);
                if(topic == null)
                {
                    topic = new Repository.Models.Topic();
                    topic.TopicName = "None";
                }
                discussions.Add(Mapper.RepoDiscussionToDiscussion(repoDiscussion, topic));
            }
            return discussions;
        }

        public async Task<List<Comment>> GetComments(string username)
        {
            List<Repository.Models.Comment> repoComments = await _repo.GetUserComments(username);
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

        public async Task<List<string>> GetFollowingMovies(string username)
        {
            List<Repository.Models.FollowingMovie> repoFollowingMovies = await _repo.GetFollowingMovies(username);
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

        public async Task<List<Review>> GetReviews(string username)
        {
            List<Repository.Models.Review> repoReviews = await _repo.GetUserReviews(username);
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

        public async Task<bool> FollowMovie(string username, string movieid)
        {
            Repository.Models.FollowingMovie repoFollowingMovie = new Repository.Models.FollowingMovie();
            repoFollowingMovie.Username = username;
            repoFollowingMovie.MovieId = movieid;
            
            return await _repo.FollowMovie(repoFollowingMovie);
        }
    }
}

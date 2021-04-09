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

        public Topic GetDiscussionTopic(int discussionId)
        {
            return _dbContext.Topics.Where(t => t.TopicName == _dbContext.DiscussionTopics
                .Where(d => d.DiscussionId == discussionId).FirstOrDefault<DiscussionTopic>().TopicName)
                .FirstOrDefault<Topic>();
        }

        public List<Repository.Models.Discussion> GetUserDiscussions(string username)
        {
            return _dbContext.Discussions.Where(d => d.Username == username).ToList();
        }

        public List<Repository.Models.Comment> GetUserComments(string username)
        {
            return _dbContext.Comments.Where(c => c.Username == username).ToList();
        }

        public List<Repository.Models.Review> GetUserReviews(string username)
        {
            return _dbContext.Reviews.Where(r => r.Username == username).ToList();
        }

        public List<FollowingMovie> GetFollowingMovies(string username)
        {
            return _dbContext.FollowingMovies.Where(f => f.Username == username).ToList();
        }

        public bool FollowMovie(FollowingMovie followingMovie)
        {
            // Make sure the username and movieid exist in the database
            if(!UserExists(followingMovie.Username))
            {
                return false;
            }
            if(!MovieExists(followingMovie.MovieId))
            {
                return false;
            }

            _dbContext.FollowingMovies.Add(followingMovie);

            if(_dbContext.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public List<Repository.Models.Review> GetMovieReviews(string movieid)
        {
            return _dbContext.Reviews.Where(r => r.MovieId == movieid).ToList();
        }

        public bool AddReview(Review repoReview)
        {
            // Make sure the username and movieid exist in the database
            if(!UserExists(repoReview.Username))
            {
                return false;
            }
            if(!MovieExists(repoReview.MovieId))
            {
                return false;
            }

            _dbContext.Reviews.Add(repoReview);

            if(_dbContext.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        private bool UserExists(string username)
        {
            return (_dbContext.Users.Where(u => u.Username == username).FirstOrDefault<User>() != null);
        }
        private bool MovieExists(string movieid)
        {
            return (_dbContext.Movies.Where(m => m.MovieId == movieid).FirstOrDefault<Movie>() != null);
        }
        private bool DiscussionExists(int discussionid)
        {
            return (_dbContext.Discussions.Where(d => d.DiscussionId == discussionid).FirstOrDefault<Discussion>() != null);
        }
        private bool TopicExists(string topicname)
        {
            return (_dbContext.Topics.Where(t => t.TopicName == topicname).FirstOrDefault<Topic>() != null);
        }
    }
}

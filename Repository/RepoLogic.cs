using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Adds the User specified in the argument to the database. Returns true iff successful.
        /// Returns false if the user already exists.
        /// </summary>
        /// <param name="repoUser"></param>
        /// <returns></returns>
        public async Task<bool> AddUser(User repoUser)
        {
            if(UserExists(repoUser.Username))
            {
                Console.WriteLine("RepoLogic.AddUser() was called for a user that already exists.");
                return false;
            }
            await _dbContext.Users.AddAsync(repoUser);

            if(await _dbContext.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates the information of the User identified by the username argument
        /// to the information in the User object. Returns true iff successful.
        /// Returns false if the user doesn't exist.
        /// </summary>
        /// <param name="repoUser"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(string username, User updatedUser)
        {
            User existingUser = _dbContext.Users.Where(u => u.Username == username).FirstOrDefault<User>();
            if(existingUser == null)
            {
                Console.WriteLine("RepoLogic.UpdateUser() was called for a user that doesn't exist.");
                return false;
            }
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Email = updatedUser.Email;

            if(await _dbContext.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the User object from the database that matches the username specified
        /// in the argument. Returns null if the username is not found.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUser(string username)
        {
            return _dbContext.Users.Where(u => u.Username == username).FirstOrDefault<User>();
        }

        /// <summary>
        /// Adds the Discussion specified in the argument to the database.
        /// Returns true iff successful.
        /// Returns false if the username or movieid referenced in the Discussion object
        /// do not already exist in their respective database tables.
        /// </summary>
        /// <param name="repoDiscussion"></param>
        /// <returns></returns>
        public async Task<bool> AddDiscussion(Discussion repoDiscussion, Topic repoTopic)
        {
            var userExists = UserExists(repoDiscussion.Username);
            if(!userExists)
            {
                Console.WriteLine("RepoLogic.AddDiscussion() was called for a user that doesn't exist.");
                return false;
            }
            var movieExists = MovieExists(repoDiscussion.MovieId);
            if(!movieExists)
            {
                Console.WriteLine("RepoLogic.AddDiscussion() was called for a movie that doesn't exist.");
                return false;
            }
            var topicExists = TopicExists(repoTopic.TopicName);
            if(topicExists)
            {
                await _dbContext.Discussions.AddAsync(repoDiscussion);

                if((await _dbContext.SaveChangesAsync()) > 0)
                {
                    int count = 0;
                    Discussion discussion;
                    while((discussion = _dbContext.Discussions.Where(d => d.MovieId == repoDiscussion.MovieId
                        && d.Username == repoDiscussion.Username && d.Subject == repoDiscussion.Subject)
                        .FirstOrDefault<Discussion>()) == null)
                    {
                        if(count > 50)
                        {
                            return true;
                        }
                        Thread.Sleep(100);
                        count += 1;
                    }
                    await AddDiscussionTopic(discussion.DiscussionId, repoTopic.TopicName);
                    return true;
                }
                return false;
            }
            else
            {
                await _dbContext.Discussions.AddAsync(repoDiscussion);

                if((await _dbContext.SaveChangesAsync()) > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Adds the DiscussionTopic defined by the discussion Id and topic name arguments
        /// to the database.
        /// Returns true iff successful.
        /// Returns false if the Discussion with the specified discussionId or the Topic with
        /// the specified topicName referenced do not already exist in their respective
        /// database tables.
        /// </summary>
        /// <param name="discussionId"></param>
        /// <param name="topicName"></param>
        /// <returns></returns>
        public async Task<bool> AddDiscussionTopic(int discussionId, string topicName)
        {
            var discussionExists = DiscussionExists(discussionId);
            if(!discussionExists)
            {
                Console.WriteLine("RepoLogic.AddDiscussionTopic() was called for a discussion id that doesn't exist.");
                return false;
            }
            var topicExists = TopicExists(topicName);
            if(!topicExists)
            {
                Console.WriteLine("RepoLogic.AddDiscussionTopic() was called for a topic that doesn't exist.");
                return false;
            }
            var discussionTopic = new DiscussionTopic();
            discussionTopic.DiscussionId = discussionId;
            discussionTopic.TopicName = topicName;

            await _dbContext.DiscussionTopics.AddAsync(discussionTopic);

            if((await _dbContext.SaveChangesAsync()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds the Comment specified in the argument to the database.
        /// Returns true iff successful.
        /// Returns false if the username or discussion ID referenced in the Comment object
        /// do not already exist in their respective database tables.
        /// </summary>
        /// <param name="repoComment"></param>
        /// <returns></returns>
        public async Task<bool> AddComment(Comment repoComment)
        {
            var userExists = UserExists(repoComment.Username);
            if(!userExists)
            {
                Console.WriteLine("RepoLogic.AddComment() was called for a user that doesn't exist.");
                return false;
            }
            var discussionExists = DiscussionExists(repoComment.DiscussionId);
            if(!discussionExists)
            {
                Console.WriteLine("RepoLogic.AddComment() was called for a discussion that doesn't exist.");
                return false;
            }

            await _dbContext.Comments.AddAsync(repoComment);

            if((await _dbContext.SaveChangesAsync()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a list of all User objects in the database. If there are no users,
        /// the list will be empty.
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        /// <summary>
        /// Returns the Topic object from the database that matches the discussionId specified
        /// in the argument. Returns null if the discussionid doesn't exist or that discussion
        /// has no topic.
        /// </summary>
        /// <param name="discussionId"></param>
        /// <returns></returns>
        public Topic GetDiscussionTopic(int discussionId)
        {
            var discussionExists = DiscussionExists(discussionId);
            if(!discussionExists)
            {
                Console.WriteLine("RepoLogic.GetDiscussionTopic() was called for a discussion that doesn't exist.");
                return null;
            }
            return _dbContext.Topics.Where(t => t.TopicName == _dbContext.DiscussionTopics
                .Where(d => d.DiscussionId == discussionId).FirstOrDefault<DiscussionTopic>().TopicName)
                .FirstOrDefault<Topic>();
        }

        /// <summary>
        /// Returns a list of all Discussion objects from the database that match the username specified
        /// in the argument. Returns null if the user doesn't exist.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<List<Discussion>> GetUserDiscussions(string username)
        {
            var userExists = UserExists(username);
            if(!userExists)
            {
                Console.WriteLine("RepoLogic.GetUserDiscussions() was called for a user that doesn't exist.");
                return null;
            }
            return await _dbContext.Discussions.Where(d => d.Username == username).ToListAsync();
        }

        /// <summary>
        /// Returns the Discussion object that match the discussionid specified in the argument.
        /// </summary>
        /// <param name="discussionid"></param>
        /// <returns></returns>
        public async Task<Discussion> GetDiscussion(int discussionid)
        {
            return await _dbContext.Discussions.Where(d => d.DiscussionId == discussionid).FirstOrDefaultAsync<Discussion>();
        }

        /// <summary>
        /// Returns a list of all Comment objects from the database that match the username specified
        /// in the argument. Returns null if the user doesn't exist.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<List<Comment>> GetUserComments(string username)
        {
            var userExists = UserExists(username);
            if(!userExists)
            {
                Console.WriteLine("RepoLogic.GetUserComments() was called for a user that doesn't exist.");
                return null;
            }
            return await _dbContext.Comments.Where(c => c.Username == username).ToListAsync();
        }

        /// <summary>
        /// Returns a list of all Review objects from the database that match the username specified
        /// in the argument. Returns null if the user doesn't exist.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<List<Review>> GetUserReviews(string username)
        {
            var userExists = UserExists(username);
            if(!userExists)
            {
                Console.WriteLine("RepoLogic.GetUserReviews() was called for a user that doesn't exist.");
                return null;
            }
            return await _dbContext.Reviews.Where(r => r.Username == username).ToListAsync();
        }

        /// <summary>
        /// Returns a list of all FollowingMovie objects from the database that match the username
        /// specified in the argument. Returns null if the user doesn't exist.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<List<FollowingMovie>> GetFollowingMovies(string username)
        {
            var userExists = UserExists(username);
            if(!userExists)
            {
                Console.WriteLine("RepoLogic.GetFollowingMovies() was called for a user that doesn't exist.");
                return null;
            }
            return await _dbContext.FollowingMovies.Where(f => f.Username == username).ToListAsync();
        }

        /// <summary>
        /// Adds the FollowingMovie specified in the argument to the database.
        /// Returns true iff successful.
        /// Returns false if the username or movieid referenced in the FollowingMovie object
        /// do not already exist in their respective database tables.
        /// </summary>
        /// <param name="followingMovie"></param>
        /// <returns></returns>
        public async Task<bool> FollowMovie(FollowingMovie followingMovie)
        {
            var userExists = UserExists(followingMovie.Username);
            if(!userExists)
            {
                Console.WriteLine("RepoLogic.FollowMovie() was called for a user that doesn't exist.");
                return false;
            }
            var movieExists = MovieExists(followingMovie.MovieId);
            if(!movieExists)
            {
                Console.WriteLine("RepoLogic.FollowMovie() was called for a movie that doesn't exist.");
                return false;
            }
            // Ensure the User is not already Following this Movie
            if((await _dbContext.FollowingMovies.Where(fm => 
                    fm.Username == followingMovie.Username 
                    && fm.MovieId == followingMovie.MovieId
                ).FirstOrDefaultAsync<FollowingMovie>()) != null)
            {
                Console.WriteLine("RepoLogic.FollowMovie() was called for a movie that the user is " +
                    "already following.");
                return false;
            }

            await _dbContext.FollowingMovies.AddAsync(followingMovie);

            if(await _dbContext.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a list of all Review objects from the database that match the movie ID specified
        /// in the argument. Returns null if the movie doesn't exist.
        /// </summary>
        /// <param name="movieid"></param>
        /// <returns></returns>
        public async Task<List<Review>> GetMovieReviews(string movieid)
        {
            var movieExists = MovieExists(movieid);
            if(!movieExists)
            {
                Console.WriteLine("RepoLogic.GetMovieReviews() was called for a movie that doesn't exist.");
                return null;
            }
            return await _dbContext.Reviews.Where(r => r.MovieId == movieid).ToListAsync();
        }

        /// <summary>
        /// Adds the Review specified in the argument to the database.
        /// If the User has already reviewed this movie, the review is replaced.
        /// Returns true iff successful.
        /// Returns false if the username or movie ID referenced in the Review object
        /// do not already exist in their respective database tables.
        /// </summary>
        /// <param name="repoReview"></param>
        /// <returns></returns>
        public async Task<bool> AddReview(Review repoReview)
        {
            var userExists = UserExists(repoReview.Username);
            if(!userExists)
            {
                Console.WriteLine("RepoLogic.AddReview() was called for a user that doesn't exist.");
                return false;
            }
            var movieExists = MovieExists(repoReview.MovieId);
            if(!movieExists)
            {
                Console.WriteLine("RepoLogic.AddReview() was called for a movie that doesn't exist.");
                return false;
            }
            // If the User has already reviewed this movie, update it
            Review review = await _dbContext.Reviews.Where(r => 
                    r.Username == repoReview.Username 
                    && r.MovieId == repoReview.MovieId).FirstOrDefaultAsync<Review>();
            if(review == null)
            {
                await _dbContext.Reviews.AddAsync(repoReview);

                if(await _dbContext.SaveChangesAsync() > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                review.Rating = repoReview.Rating;
                review.Review1 = repoReview.Review1;
                
                if(await _dbContext.SaveChangesAsync() > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Adds the Movie specified in the argument to the database.
        /// Returns true iff successful.
        /// </summary>
        /// <param name="movieid"></param>
        /// <returns></returns>
        public async Task<bool> AddMovie(string movieid)
        {
            if(MovieExists(movieid))
            {
                Console.WriteLine("RepoLogic.AddMovie() was called for a movie that doesn't exist.");
                return false;
            }
            Repository.Models.Movie movie = new Repository.Models.Movie();
            movie.MovieId = movieid;
            await _dbContext.Movies.AddAsync(movie);

            if(await _dbContext.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a list of all Topic objects in the database.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Topic>> GetTopics()
        {
            return await _dbContext.Topics.ToListAsync();
        }

        /// <summary>
        /// Returns a list of all Discussion objects from the database that match the movie ID specified
        /// in the argument. Returns null if the movie doesn't exist.
        /// </summary>
        /// <param name="movieid"></param>
        /// <returns></returns>
        public async Task<List<Discussion>> GetMovieDiscussions(string movieid)
        {
            var movieExists = MovieExists(movieid);
            if(!movieExists)
            {
                Console.WriteLine("RepoLogic.GetMovieDiscussions() was called for a movie that doesn't exist.");
                return null;
            }
            return await _dbContext.Discussions.Where(d => d.MovieId == movieid).ToListAsync();
        }

        /// <summary>
        /// Returns a list of all Comment objects from the database that match the discussion ID specified
        /// in the argument. Returns null if the discussion doesn't exist.
        /// </summary>
        /// <param name="discussionid"></param>
        /// <returns></returns>
        public async Task<List<Comment>> GetMovieComments(int discussionid)
        {
            var discussionExists = DiscussionExists(discussionid);
            if(!discussionExists)
            {
                Console.WriteLine("RepoLogic.GetMovieComments() was called for a discussion that doesn't exist.");
                return null;
            }
            var commentList = await _dbContext.Comments.Where(c => c.DiscussionId == discussionid).ToListAsync();
            return commentList;
        }

        /// <summary>
        /// Gets the value(s) of an existing setting in the database with a matching key string.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Setting GetSetting(string key)
        {
            return _dbContext.Settings.Where(s => s.Setting1 == key).FirstOrDefault<Setting>();
        }

        /// <summary>
        /// Creates a new setting entry or updates the value(s) of an existing setting
        /// in the database.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public async Task<bool> SetSetting(Setting setting)
        {
            if(setting == null || setting.Setting1.Length < 1)
            {
                Console.WriteLine("RepoLogic.SetSetting() was called with a null or invalid setting.");
                return false;
            }
            if(SettingExists(setting.Setting1))
            {
                Setting existentSetting = await _dbContext.Settings.Where(
                    s => s.Setting1 == setting.Setting1).FirstOrDefaultAsync<Setting>();
                if(setting.IntValue != null)
                {
                    existentSetting.IntValue = setting.IntValue;
                }
                if(setting.StringValue != null)
                {
                    existentSetting.StringValue = setting.StringValue;
                }
                if(await _dbContext.SaveChangesAsync() > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                await _dbContext.Settings.AddAsync(setting);

                if(await _dbContext.SaveChangesAsync() > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Returns true iff the username, specified in the argument, exists in the database's Users table.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private bool UserExists(string username)
        {
            return (_dbContext.Users.Where(u => u.Username == username).FirstOrDefault<User>() != null);
        }

        /// <summary>
        /// Returns true iff the movie ID, specified in the argument, exists in the database's Movies table.
        /// </summary>
        /// <param name="movieid"></param>
        /// <returns></returns>
        private bool MovieExists(string movieid)
        {
            return (_dbContext.Movies.Where(m => m.MovieId == movieid).FirstOrDefault<Movie>() != null);
        }

        /// <summary>
        /// Returns true iff the discussion ID, specified in the argument, exists in the database's Discussions table.
        /// </summary>
        /// <param name="discussionid"></param>
        /// <returns></returns>
        private bool DiscussionExists(int discussionid)
        {
            return (_dbContext.Discussions.Where(d => d.DiscussionId == discussionid).FirstOrDefault<Discussion>() != null);
        }

        /// <summary>
        /// Returns true iff the Topic name, specified in the argument, exists in the database's Topics table.
        /// </summary>
        /// <param name="discussionid"></param>
        /// <returns></returns>
        private bool TopicExists(string topicName)
        {
            return (_dbContext.Topics.Where(t => t.TopicName == topicName).FirstOrDefault<Topic>() != null);
        }

        /// <summary>
        /// Returns true iff the setting key, specified in the argument, exists in the database's Settings table.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool SettingExists(string key)
        {
            return (_dbContext.Settings.Where(s => s.Setting1 == key).FirstOrDefault<Setting>() != null);
        }
    }
}

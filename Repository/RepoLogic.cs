using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Adds the User specified in the argument to the database.
        /// </summary>
        /// <param name="repoUser"></param>
        /// <returns></returns>
        public async Task<bool> AddUser(User repoUser)
        {
            if(UserExists(repoUser.Username))
            {
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
        /// Returns the User object from the database that matches the username specified
        /// in the argument.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUser(string username)
        {
            return _dbContext.Users.Where(a => a.Username == username).FirstOrDefault<User>();
        }

        /// <summary>
        /// Adds the Discussion specified in the argument to the database.
        /// Returns true iff successful.
        /// Returns false if the username or movieid referenced in the Discussion object
        /// do not already exist in their respective database tables.
        /// </summary>
        /// <param name="repoDiscussion"></param>
        /// <returns></returns>
        public async Task<bool> AddDiscussion(Discussion repoDiscussion)
        {
            var userExists = UserExists(repoDiscussion.Username);
            if(!userExists)
            {
                return false;
            }
            var movieExists = MovieExists(repoDiscussion.MovieId);
            if(!movieExists)
            {
                return false;
            }

            await _dbContext.Discussions.AddAsync(repoDiscussion);

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
                return false;
            }
            var discussionExists = DiscussionExists(repoComment.DiscussionId);
            if(!discussionExists)
            {
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
        /// Returns a list of all User objects in the database.
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        /// <summary>
        /// Returns the Topic object from the database that matches the discussionId specified
        /// in the argument.
        /// </summary>
        /// <param name="discussionId"></param>
        /// <returns></returns>
        public Topic GetDiscussionTopic(int discussionId)
        {
            return _dbContext.Topics.Where(t => t.TopicName == _dbContext.DiscussionTopics
                .Where(d => d.DiscussionId == discussionId).FirstOrDefault<DiscussionTopic>().TopicName)
                .FirstOrDefault<Topic>();
        }

        /// <summary>
        /// Returns a list of all Discussion objects from the database that match the username specified
        /// in the argument.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<List<Discussion>> GetUserDiscussions(string username)
        {
            return await _dbContext.Discussions.Where(d => d.Username == username).ToListAsync();
        }

        /// <summary>
        /// Returns a list of all Comment objects from the database that match the username specified
        /// in the argument.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<List<Comment>> GetUserComments(string username)
        {
            return await _dbContext.Comments.Where(c => c.Username == username).ToListAsync();
        }

        /// <summary>
        /// Returns a list of all Review objects from the database that match the username specified
        /// in the argument.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<List<Review>> GetUserReviews(string username)
        {
            return await _dbContext.Reviews.Where(r => r.Username == username).ToListAsync();
        }

        /// <summary>
        /// Returns a list of all FollowingMovie objects from the database that match the username
        /// specified in the argument.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<List<FollowingMovie>> GetFollowingMovies(string username)
        {
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
                return false;
            }
            var movieExists = MovieExists(followingMovie.MovieId);
            if(!movieExists)
            {
                return false;
            }
            // Ensure the User is not already Following this Movie
            if((await _dbContext.FollowingMovies.Where(fm => 
                    fm.Username == followingMovie.Username 
                    && fm.MovieId == followingMovie.MovieId
                ).FirstOrDefaultAsync<FollowingMovie>()) != null)
            {
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
        /// in the argument.
        /// </summary>
        /// <param name="movieid"></param>
        /// <returns></returns>
        public async Task<List<Review>> GetMovieReviews(string movieid)
        {
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
                return false;
            }
            var movieExists = MovieExists(repoReview.MovieId);
            if(!movieExists)
            {
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
        /// in the argument.
        /// </summary>
        /// <param name="movieid"></param>
        /// <returns></returns>
        public async Task<List<Discussion>> GetMovieDiscussions(string movieid)
        {
            return await _dbContext.Discussions.Where(d => d.MovieId == movieid).ToListAsync();
        }

        /// <summary>
        /// Returns a list of all Comment objects from the database that match the discussion ID specified
        /// in the argument.
        /// </summary>
        /// <param name="discussionid"></param>
        /// <returns></returns>
        public async Task<List<Comment>> GetMovieComments(int discussionid)
        {
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

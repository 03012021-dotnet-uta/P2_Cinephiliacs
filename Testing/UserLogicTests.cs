using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Interfaces;
using CineAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using Repository;
using Xunit;
using Xunit.Abstractions;

namespace Testing
{
    public class UserLogicTests
    {
        readonly DbContextOptions<Repository.Models.Cinephiliacs_DbContext> dbOptions =
            new DbContextOptionsBuilder<Repository.Models.Cinephiliacs_DbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        [Fact]
        public async Task UserTest()
        {
            GlobalModels.User inputGMUser;
            GlobalModels.User outputGMUser;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            // Seed the test database
            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                inputGMUser = BusinessLogic.Mapper.RepoUserToUser(dataSetA.User);

                RepoLogic repoLogic = new RepoLogic(context);

                // Test CreateUser()
                IUserLogic userLogic = new UserLogic(repoLogic);
                UserController userController = new UserController(userLogic);
                await userController.CreateUser(inputGMUser);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {                
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetUser()
                IUserLogic userLogic = new UserLogic(repoLogic);
                UserController userController = new UserController(userLogic);
                outputGMUser = userController.GetUser(dataSetA.User.Username).Value;
            }

            Assert.Equal(inputGMUser, outputGMUser);
        }

        [Fact]
        public async Task UsersTest()
        {
            GlobalModels.User inputGMUser;
            GlobalModels.User outputGMUser;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            // Seed the test database
            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                inputGMUser = BusinessLogic.Mapper.RepoUserToUser(dataSetA.User);

                RepoLogic repoLogic = new RepoLogic(context);

                IUserLogic userLogic = new UserLogic(repoLogic);
                UserController userController = new UserController(userLogic);
                await userController.CreateUser(inputGMUser);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {                
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetUsers()
                IUserLogic userLogic = new UserLogic(repoLogic);
                UserController userController = new UserController(userLogic);
                List<GlobalModels.User> gmUserList = (await userController.Get()).Value;
                outputGMUser = gmUserList
                    .FirstOrDefault<GlobalModels.User>(u => u.Username == dataSetA.User.Username);
            }

            Assert.Equal(inputGMUser, outputGMUser);
        }

        [Fact]
        public async Task FollowMovieTest()
        {
            string outputFollowingMovieId;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            // Seed the test database
            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);
                // Load Database entries for the object-under-test's foreign keys
                await repoLogic.AddUser(dataSetA.User);
                await repoLogic.AddMovie(dataSetA.Movie.MovieId);

                // Test FollowMovie()
                IUserLogic userLogic = new UserLogic(repoLogic);
                UserController userController = new UserController(userLogic);
                await userController.FollowMovie(dataSetA.FollowingMovie.Username, dataSetA.FollowingMovie.MovieId);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {                
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetFollowingMovies()
                IUserLogic userLogic = new UserLogic(repoLogic);
                UserController userController = new UserController(userLogic);
                List<string> followingMovieList = (await userController.GetFollowingMovies(dataSetA.User.Username)).Value;
                outputFollowingMovieId = followingMovieList.FirstOrDefault<string>();
            }

            Assert.Equal(dataSetA.FollowingMovie.MovieId, outputFollowingMovieId);
        }

        [Fact]
        public async Task ReviewsTest()
        {
            GlobalModels.Review inputGMReview;
            GlobalModels.Review outputGMReview;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            // Seed the test database
            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                inputGMReview = BusinessLogic.Mapper.RepoReviewToReview(dataSetA.Review);

                RepoLogic repoLogic = new RepoLogic(context);
                // Load Database entries for the object-under-test's foreign keys
                await repoLogic.AddUser(dataSetA.User);
                await repoLogic.AddMovie(dataSetA.Movie.MovieId);

                IMovieLogic movieLogic = new MovieLogic(repoLogic);
                MovieController movieController = new MovieController(movieLogic);
                await movieController.CreateReview(inputGMReview);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetReviews()
                IUserLogic userLogic = new UserLogic(repoLogic);
                UserController userController = new UserController(userLogic);
                List<GlobalModels.Review> gmReviewList = (await userController.GetReviews(dataSetA.User.Username)).Value;
                outputGMReview = gmReviewList
                    .FirstOrDefault<GlobalModels.Review>(r => r.Username == dataSetA.User.Username);
            }

            Assert.Equal(inputGMReview, outputGMReview);
        }

        [Fact]
        public async Task DiscussionsTest()
        {
            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            GlobalModels.Discussion inputGMDiscussion = BusinessLogic.Mapper.RepoDiscussionToDiscussion(dataSetA.Discussion, dataSetA.Topic);
            GlobalModels.Discussion outputGMDiscussion;

            // Seed the test database
            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var newDiscussion = new GlobalModels.NewDiscussion(inputGMDiscussion);

                RepoLogic repoLogic = new RepoLogic(context);
                // Add Database entries for the object-under-test's foreign keys
                await repoLogic.AddUser(dataSetA.User);
                await repoLogic.AddMovie(dataSetA.Movie.MovieId);

                IForumLogic forumLogic = new ForumLogic(repoLogic);
                ForumController forumController = new ForumController(forumLogic);
                await forumController.CreateDiscussion(newDiscussion);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {                
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetDiscussions()
                IUserLogic userLogic = new UserLogic(repoLogic);
                UserController userController = new UserController(userLogic);
                List<GlobalModels.Discussion> gmDiscussionList = (await userController.GetDiscussions(dataSetA.User.Username)).Value;
                outputGMDiscussion = gmDiscussionList
                    .FirstOrDefault<GlobalModels.Discussion>(d => d.Username == dataSetA.User.Username);
            }

            Assert.Equal(inputGMDiscussion, outputGMDiscussion);
        }

        [Fact]
        public async Task CommentsTest()
        {
            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            GlobalModels.Comment inputGMComment = BusinessLogic.Mapper.RepoCommentToComment(dataSetA.Comment);
            GlobalModels.Comment outputGMComment;

            // Seed the test database
            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var newComment = new GlobalModels.NewComment(inputGMComment);

                RepoLogic repoLogic = new RepoLogic(context);
                // Add Database entries for the object-under-test's foreign keys
                await repoLogic.AddUser(dataSetA.User);
                await repoLogic.AddMovie(dataSetA.Movie.MovieId);
                await repoLogic.AddDiscussion(dataSetA.Discussion, dataSetA.Topic);

                IForumLogic forumLogic = new ForumLogic(repoLogic);
                ForumController forumController = new ForumController(forumLogic);
                await forumController.CreateComment(newComment);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetComments()
                IUserLogic userLogic = new UserLogic(repoLogic);
                UserController userController = new UserController(userLogic);
                List<GlobalModels.Comment> gmCommentList = (await userController.GetComments(dataSetA.User.Username)).Value;
                outputGMComment = gmCommentList
                    .FirstOrDefault<GlobalModels.Comment>(c => c.Username == dataSetA.User.Username);
            }

            Assert.Equal(inputGMComment, outputGMComment);
        }

    }
}

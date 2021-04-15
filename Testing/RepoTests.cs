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
    public class RepoTests
    {
        readonly DbContextOptions<Repository.Models.Cinephiliacs_DbContext> dbOptions =
            new DbContextOptionsBuilder<Repository.Models.Cinephiliacs_DbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        [Fact]
        public async Task AddUserTwice()
        {
            bool result;
            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            // Seed the test database
            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                GlobalModels.User inputGMUser = BusinessLogic.Mapper.RepoUserToUser(dataSetA.User);

                RepoLogic repoLogic = new RepoLogic(context);

                // Test CreateUser()
                IUserLogic userLogic = new UserLogic(repoLogic);
                UserController userController = new UserController(userLogic);
                await userController.CreateUser(inputGMUser);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {                
                RepoLogic repoLogic = new RepoLogic(context);

                // Test AddUser()
                result = await repoLogic.AddUser(dataSetA.User);
            }

            Assert.False(result);
        }

        [Fact]
        public async Task NoUserFollowMovieTest()
        {
            bool result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test FollowMovie() without User dependency
                result = await repoLogic.FollowMovie(dataSetA.FollowingMovie);
            }

            Assert.False(result);
        }

        [Fact]
        public async Task NoUserAddCommentTest()
        {
            bool result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test AddComment() without User dependency
                result = await repoLogic.AddComment(dataSetA.Comment);
            }

            Assert.False(result);
        }

        [Fact]
        public async Task NoUserAddDiscussionTest()
        {
            bool result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test AddDiscussion() without User dependency
                result = await repoLogic.AddDiscussion(dataSetA.Discussion, dataSetA.Topic);
            }

            Assert.False(result);
        }

        [Fact]
        public async Task NoUserAddReviewTest()
        {
            bool result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test AddDiscussion() without User dependency
                result = await repoLogic.AddReview(dataSetA.Review);
            }

            Assert.False(result);
        }

        [Fact]
        public async Task NoMovieAddDiscussionTest()
        {
            bool result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);
                await repoLogic.AddUser(dataSetA.User);

                // Test AddDiscussion() without Movie dependency
                result = await repoLogic.AddDiscussion(dataSetA.Discussion, dataSetA.Topic);
            }

            Assert.False(result);
        }

        [Fact]
        public async Task NoMovieAddCommentTest()
        {
            bool result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);
                await repoLogic.AddUser(dataSetA.User);

                // Test AddComment() without Movie dependency
                result = await repoLogic.AddComment(dataSetA.Comment);
            }

            Assert.False(result);
        }

        [Fact]
        public async Task NoUserGetReviewsTest()
        {
            object result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetUserReviews() without User dependency
                result = await repoLogic.GetUserReviews(dataSetA.User.Username);
            }

            Assert.Null(result);
        }

        [Fact]
        public async Task NoUserGetDiscussionsTest()
        {
            object result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetUserDiscussions() without User dependency
                result = await repoLogic.GetUserDiscussions(dataSetA.User.Username);
            }

            Assert.Null(result);
        }

        [Fact]
        public async Task NoUserGetCommentsTest()
        {
            object result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetUserComments() without User dependency
                result = await repoLogic.GetUserComments(dataSetA.User.Username);
            }

            Assert.Null(result);
        }

        [Fact]
        public async Task NoUserUpdateUserTest()
        {
            bool result;

            Repository.Models.User updatedRepoUser = new Repository.Models.User();

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");
            updatedRepoUser.Username = dataSetA.User.Username;
            updatedRepoUser.FirstName = "Steve";
            updatedRepoUser.LastName = dataSetA.User.LastName;
            updatedRepoUser.Email = dataSetA.User.Email;
            updatedRepoUser.Permissions = dataSetA.User.Permissions;
            updatedRepoUser.Username = dataSetA.User.Username;

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test UpdateUser() without User dependency
                result = await repoLogic.UpdateUser(dataSetA.User.Username, updatedRepoUser);
            }

            Assert.False(result);
        }

        [Fact]
        public async Task NoUserGetFollowingMovieTest()
        {
            object result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetFollowingMovies() without User dependency
                result = await repoLogic.GetFollowingMovies(dataSetA.User.Username);
            }

            Assert.Null(result);
        }

        [Fact]
        public async Task NoMovieFollowMovieTest()
        {
            bool result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);
                await repoLogic.AddUser(dataSetA.User);

                // Test FollowMovie() without Movie dependency
                result = await repoLogic.FollowMovie(dataSetA.FollowingMovie);
            }

            Assert.False(result);
        }

        [Fact]
        public async Task FollowMovieTwiceTest()
        {
            bool result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);
                await repoLogic.AddUser(dataSetA.User);
                await repoLogic.AddMovie(dataSetA.Movie.MovieId);

                await repoLogic.FollowMovie(dataSetA.FollowingMovie);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                RepoLogic repoLogic = new RepoLogic(context);

                // Test FollowMovie() a second time with same input
                result = await repoLogic.FollowMovie(dataSetA.FollowingMovie);
            }

            Assert.False(result);
        }

        [Fact]
        public async Task NoDiscAddDiscussionTopicTest()
        {
            bool result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test AddDiscussionTopic() without Discussion dependency
                result = await repoLogic.AddDiscussionTopic(dataSetA.DiscussionTopic.DiscussionId, dataSetA.DiscussionTopic.TopicName);
            }

            Assert.False(result);
        }

        [Fact]
        public async Task NoTopicAddDiscussionTopicTest()
        {
            bool result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);
                await repoLogic.AddDiscussion(dataSetA.Discussion, dataSetA.Topic);

                // Test AddDiscussionTopic() without Topic dependency
                result = await repoLogic.AddDiscussionTopic(dataSetA.DiscussionTopic.DiscussionId, dataSetA.DiscussionTopic.TopicName);
            }

            Assert.False(result);
        }

        [Fact]
        public void NoDiscGetDiscussionTopicTest()
        {
            object result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetDiscussionTopic() without Discussion dependency
                result = repoLogic.GetDiscussionTopic(dataSetA.DiscussionTopic.DiscussionId);
            }

            Assert.Null(result);
        }

        [Fact]
        public async Task NoDiscGetMovieCommentsTest()
        {
            object result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetMovieComments() without Discussion dependency
                result = await repoLogic.GetMovieComments(dataSetA.DiscussionTopic.DiscussionId);
            }

            Assert.Null(result);
        }

        [Fact]
        public async Task NoMovieGetMovieDiscussionsTest()
        {
            object result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetMovieDiscussions() without Movie dependency
                result = await repoLogic.GetMovieDiscussions(dataSetA.Movie.MovieId);
            }

            Assert.Null(result);
        }

        [Fact]
        public async Task NoMovieGetMovieReviewsTest()
        {
            object result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetMovieReviews() without Movie dependency
                result = await repoLogic.GetMovieReviews(dataSetA.Movie.MovieId);
            }

            Assert.Null(result);
        }

        [Fact]
        public async Task NoMovieAddReviewTest()
        {
            bool result;

            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {              
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test AddReview() without Movie dependency
                result = await repoLogic.AddReview(dataSetA.Review);
            }

            Assert.False(result);
        }
    }
}

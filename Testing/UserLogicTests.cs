using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
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
                UserLogic userLogic = new UserLogic(repoLogic);
                await userLogic.CreateUser(inputGMUser);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {                
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetUser()
                UserLogic userLogic = new UserLogic(repoLogic);
                outputGMUser = userLogic.GetUser(dataSetA.User.Username);
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

                UserLogic userLogic = new UserLogic(repoLogic);
                await userLogic.CreateUser(inputGMUser);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {                
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetUsers()
                UserLogic userLogic = new UserLogic(repoLogic);
                List<GlobalModels.User> gmUserList = await userLogic.GetUsers();
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
                UserLogic userLogic = new UserLogic(repoLogic);
                await userLogic.FollowMovie(dataSetA.FollowingMovie.Username, dataSetA.FollowingMovie.MovieId);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {                
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetFollowingMovies()
                UserLogic userLogic = new UserLogic(repoLogic);
                List<string> followingMovieList = await userLogic.GetFollowingMovies(dataSetA.User.Username);
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

                MovieLogic movieLogic = new MovieLogic(repoLogic);
                await movieLogic.CreateReview(inputGMReview);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetReviews()
                UserLogic userLogic = new UserLogic(repoLogic);
                List<GlobalModels.Review> gmReviewList = await userLogic.GetReviews(dataSetA.User.Username);
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

                ForumLogic forumLogic = new ForumLogic(repoLogic);
                await forumLogic.CreateDiscussion(newDiscussion);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {                
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetDiscussions()
                UserLogic userLogic = new UserLogic(repoLogic);
                List<GlobalModels.Discussion> gmDiscussionList = await userLogic.GetDiscussions(dataSetA.User.Username);
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
                await repoLogic.AddDiscussion(dataSetA.Discussion);
                
                ForumLogic forumLogic = new ForumLogic(repoLogic);
                await forumLogic.CreateComment(newComment);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetComments()
                UserLogic userLogic = new UserLogic(repoLogic);
                List<GlobalModels.Comment> gmCommentList = await userLogic.GetComments(dataSetA.User.Username);
                outputGMComment = gmCommentList
                    .FirstOrDefault<GlobalModels.Comment>(c => c.Username == dataSetA.User.Username);
            }

            Assert.Equal(inputGMComment, outputGMComment);
        }

    }
}

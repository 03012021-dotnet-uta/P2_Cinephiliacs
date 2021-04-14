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
                result = await repoLogic.AddDiscussion(dataSetA.Discussion);
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
    }
}
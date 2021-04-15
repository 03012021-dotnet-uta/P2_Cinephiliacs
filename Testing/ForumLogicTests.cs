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
    public class ForumLogicTests
    {
        readonly DbContextOptions<Repository.Models.Cinephiliacs_DbContext> dbOptions =
            new DbContextOptionsBuilder<Repository.Models.Cinephiliacs_DbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        [Fact]
        public async Task GetDiscussionsTest()
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

                // Test CreateDiscussion()
                IForumLogic forumLogic = new ForumLogic(repoLogic);
                ForumController forumController = new ForumController(forumLogic);
                await forumController.CreateDiscussion(newDiscussion);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {                
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetDiscussions()
                IForumLogic forumLogic = new ForumLogic(repoLogic);
                ForumController forumController = new ForumController(forumLogic);
                List<GlobalModels.Discussion> gmDiscussionList = (await forumController.GetDiscussions(dataSetA.Discussion.MovieId)).Value;
                outputGMDiscussion = gmDiscussionList
                    .FirstOrDefault<GlobalModels.Discussion>(d => d.Movieid == dataSetA.Discussion.MovieId);
            }

            Assert.Equal(inputGMDiscussion, outputGMDiscussion);
        }

        [Fact]
        public async Task GetDiscussionTest()
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
                await repoLogic.AddTopic(dataSetA.Topic);

                // Test CreateDiscussion()
                IForumLogic forumLogic = new ForumLogic(repoLogic);
                ForumController forumController = new ForumController(forumLogic);
                await forumController.CreateDiscussion(newDiscussion);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {                
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetDiscussions()
                IForumLogic forumLogic = new ForumLogic(repoLogic);
                ForumController forumController = new ForumController(forumLogic);
                outputGMDiscussion = (await forumController.GetDiscussion(dataSetA.Discussion.DiscussionId)).Value;
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
                
                // Test CreateComment()
                IForumLogic forumLogic = new ForumLogic(repoLogic);
                ForumController forumController = new ForumController(forumLogic);
                await forumController.CreateComment(newComment);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetComments()
                IForumLogic forumLogic = new ForumLogic(repoLogic);
                ForumController forumController = new ForumController(forumLogic);
                List<GlobalModels.Comment> gmCommentList = (await forumController.GetComments(dataSetA.Comment.DiscussionId)).Value;
                outputGMComment = gmCommentList
                    .FirstOrDefault<GlobalModels.Comment>(c => c.Discussionid == dataSetA.Comment.DiscussionId);
            }

            Assert.Equal(inputGMComment, outputGMComment);
        }

        [Fact]
        public async Task CommentsPageTest()
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
                
                // Test CreateComment()
                IForumLogic forumLogic = new ForumLogic(repoLogic);
                ForumController forumController = new ForumController(forumLogic);
                await forumController.CreateComment(newComment);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetComments()
                IForumLogic forumLogic = new ForumLogic(repoLogic);
                ForumController forumController = new ForumController(forumLogic);
                await forumController.SetCommentsPageSize(10);
                List<GlobalModels.Comment> gmCommentList = (await forumController.GetCommentsPage(dataSetA.Comment.DiscussionId, 1)).Value;
                outputGMComment = gmCommentList
                    .FirstOrDefault<GlobalModels.Comment>(c => c.Discussionid == dataSetA.Comment.DiscussionId);
            }

            Assert.Equal(inputGMComment, outputGMComment);
        }
        
        [Fact]
        public async Task TopicTest()
        {
            RelatedDataSet dataSetA = new RelatedDataSet("JimmyJimerson", "ab10101010", "Theory");

            string outputTopicName;

            // Seed the test database
            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Topics.Add(dataSetA.Topic);
                context.SaveChanges();
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                RepoLogic repoLogic = new RepoLogic(context);
                IForumLogic forumLogic = new ForumLogic(repoLogic);
                ForumController forumController = new ForumController(forumLogic);

                List<string> topicNames = (await forumController.GetTopics()).Value;
                outputTopicName = topicNames[0];
            }

            Assert.Equal(dataSetA.Topic.TopicName, outputTopicName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.EntityFrameworkCore;
using Repository;
using Xunit;

namespace Testing
{
    public class ForumLogicTests
    {
        DbContextOptions<Repository.Models.Cinephiliacs_DbContext> dbOptions =
            new DbContextOptionsBuilder<Repository.Models.Cinephiliacs_DbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        [Fact]
        public async Task DiscussionTest()
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
                ForumLogic forumLogic = new ForumLogic(repoLogic);
                await forumLogic.CreateDiscussion(newDiscussion);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {                
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetDiscussions()
                ForumLogic forumLogic = new ForumLogic(repoLogic);
                List<GlobalModels.Discussion> gmDiscussionList = await forumLogic.GetDiscussions(dataSetA.Discussion.MovieId);
                outputGMDiscussion = gmDiscussionList.Where(d => d.Movieid == dataSetA.Discussion.MovieId)
                    .FirstOrDefault<GlobalModels.Discussion>();
            }

            Assert.Equal(inputGMDiscussion, outputGMDiscussion);
        }

        [Fact]
        public async Task CommentTest()
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
                
                // Test CreateComment()
                ForumLogic forumLogic = new ForumLogic(repoLogic);
                await forumLogic.CreateComment(newComment);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetComments()
                ForumLogic forumLogic = new ForumLogic(repoLogic);
                List<GlobalModels.Comment> gmCommentList = await forumLogic.GetComments(dataSetA.Comment.DiscussionId);
                outputGMComment = gmCommentList.Where(c => c.Discussionid == dataSetA.Comment.DiscussionId)
                    .FirstOrDefault<GlobalModels.Comment>();
            }

            Assert.Equal(inputGMComment, outputGMComment);
        }
    }
}

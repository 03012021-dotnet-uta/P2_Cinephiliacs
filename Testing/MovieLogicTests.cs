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
    public class MovieLogicTests
    {
        DbContextOptions<Repository.Models.Cinephiliacs_DbContext> dbOptions =
            new DbContextOptionsBuilder<Repository.Models.Cinephiliacs_DbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

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

                // Test CreateReview()
                MovieLogic movieLogic = new MovieLogic(repoLogic);
                await movieLogic.CreateReview(inputGMReview);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                RepoLogic repoLogic = new RepoLogic(context);

                // Test GetReviews()
                MovieLogic movieLogic = new MovieLogic(repoLogic);
                List<GlobalModels.Review> gmReviewList = await movieLogic.GetReviews(dataSetA.Movie.MovieId);
                outputGMReview = gmReviewList.Where(r => r.Movieid == dataSetA.Movie.MovieId)
                    .FirstOrDefault<GlobalModels.Review>();
            }

            Assert.Equal(inputGMReview, outputGMReview);
        }
        
        [Fact]
        public async Task MovieTest()
        {
            string inputMovieId = "ab10101010";
            string outputMovieId;

            // Seed the test database
            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);

                // Test CreateMovie()
                MovieLogic movieLogic = new MovieLogic(repoLogic);
                await movieLogic.CreateMovie(inputMovieId);
            }

            using(var context = new Repository.Models.Cinephiliacs_DbContext(dbOptions))
            {
                RepoLogic repoLogic = new RepoLogic(context);
                MovieLogic movieLogic = new MovieLogic(repoLogic);

                var gmMovie = context.Movies.Where(m => m.MovieId == inputMovieId)
                    .FirstOrDefault<Repository.Models.Movie>();
                outputMovieId = gmMovie.MovieId;
            }

            Assert.Equal(inputMovieId, outputMovieId);
        }
    }
}
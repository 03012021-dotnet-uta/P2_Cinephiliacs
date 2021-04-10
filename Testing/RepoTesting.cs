using System;
using System.Linq;
using System.Threading.Tasks;
using CineAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Models;
using Xunit;

namespace Testing
{
    public class RepoTesting
    {
        DbContextOptions<Cinephiliacs_DbContext> dbOptions = new DbContextOptionsBuilder<Cinephiliacs_DbContext>()
        .UseInMemoryDatabase(databaseName: "CineTestDb").Options;

        public async Task AddMovieTest()
        {
            string seedMovieId = "ab10101010";
            using(var context = new Cinephiliacs_DbContext(dbOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepoLogic repoLogic = new RepoLogic(context);
                await repoLogic.AddMovie(seedMovieId);
                context.SaveChanges();
            }

            string resultMovieId;
            using(var context = new Cinephiliacs_DbContext(dbOptions))
            {
                context.Database.EnsureCreated();

                Movie movie = context.Movies.Where(m => m.MovieId == seedMovieId).FirstOrDefault<Movie>();
                resultMovieId = movie.MovieId;
            }

            Assert.Equal(seedMovieId, resultMovieId);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using GlobalModels;

namespace BusinessLogic.Interfaces
{
    public interface IMovieLogic
    {
        /// <summary>
        /// Adds a new Review Object to storage.
        /// Returns true if sucessful; false otherwise.
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        public Task<bool> CreateReview(Review review);

        /// <summary>
        /// Returns a list of every Review object whose Movieid is equal to
        /// the movieid argument.
        /// </summary>
        /// <param name="movieid"></param>
        /// <returns></returns>
        public Task<List<Review>> GetReviews(string movieid);

        /// <summary>
        /// Adds a new Movie Object to storage.
        /// Returns true if sucessful; false otherwise.
        /// </summary>
        /// <param name="movieid"></param>
        /// <returns></returns>
        public Task<bool> CreateMovie(string movieid);
    }
}
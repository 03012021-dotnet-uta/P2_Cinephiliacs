using System.Collections.Generic;
using GlobalModels;

namespace BusinessLogic.Interfaces
{
    public interface IMovieLogic
    {
        bool NewReview(Review review);
        List<Review> GetReviews(string movieid);
        bool NewMovie(string movieid);
    }
}
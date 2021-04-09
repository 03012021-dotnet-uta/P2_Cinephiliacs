using System;
using System.Collections.Generic;
using GlobalModels;
using Repository;

namespace BusinessLogic
{
    public class MovieLogic : Interfaces.IMovieLogic
    {
        private readonly RepoLogic _repo;
        
        public MovieLogic(RepoLogic repo)
        {
            _repo = repo;
        }

        public List<Review> GetReviews(string movieid)
        {
            var repoReviews = _repo.GetMovieReviews(movieid);
            if(repoReviews == null)
            {
                return null;
            }

            var reviews = new List<Review>();
            foreach (var repoReview in repoReviews)
            {
                reviews.Add(Mapper.RepoReviewToReview(repoReview));
            }
            return reviews;
        }

        public bool NewMovie(string movieid)
        {
            return _repo.AddMovie(movieid);
        }

        public bool NewReview(Review review)
        {
            var repoReview = Mapper.ReviewToRepoReview(review);
            return _repo.AddReview(repoReview);
        }
    }
}

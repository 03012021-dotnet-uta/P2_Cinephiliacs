using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<List<Review>> GetReviews(string movieid)
        {
            var repoReviews = await _repo.GetMovieReviews(movieid);
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

        public async Task<bool> CreateMovie(string movieid)
        {
            return await _repo.AddMovie(movieid);
        }

        public async Task<bool> CreateReview(Review review)
        {
            var repoReview = Mapper.ReviewToRepoReview(review);
            return await _repo.AddReview(repoReview);
        }
    }
}

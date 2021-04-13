using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Review>> GetReviewsPage(string movieid, int page, string sortorder)
        {
            if(page < 1)
            {
                return null;
            }

            Repository.Models.Setting pageSizeSetting = _repo.GetSetting("reviewspagesize");
            int pageSize = pageSizeSetting.IntValue ?? default(int);
            if(pageSize < 1)
            {
                return null;
            }

            List<Repository.Models.Review> repoReviews = await _repo.GetMovieReviews(movieid);

            if(repoReviews == null)
            {
                return null;
            }

            // Sort the list of Reviews
            switch (sortorder)
            {
                case "ratingasc":
                    repoReviews = repoReviews.OrderBy(r => r.Rating).ToList<Repository.Models.Review>();
                break;
                case "ratingdsc":
                    repoReviews = repoReviews.OrderByDescending(r => r.Rating).ToList<Repository.Models.Review>();
                break;
                case "timeasc":
                    repoReviews = repoReviews.OrderBy(r => r.CreationTime).ToList<Repository.Models.Review>();
                break;
                case "timedsc":
                    repoReviews = repoReviews.OrderByDescending(r => r.CreationTime).ToList<Repository.Models.Review>();
                break;
                default:
                    return null;
            }

            int numberOfReviews = repoReviews.Count;
            int startIndex = pageSize * (page - 1);

            if(startIndex > numberOfReviews - 1)
            {
                return null;
            }

            int endIndex = startIndex + pageSize - 1;
            if(endIndex > numberOfReviews - 1)
            {
                endIndex = numberOfReviews - 1;
            }

            List<Review> reviews = new List<Review>();

            for (int i = startIndex; i <= endIndex; i++)
            {
                reviews.Add(Mapper.RepoReviewToReview(repoReviews[i]));
            }
            return reviews;
        }

        public async Task<bool> SetReviewsPageSize(int pagesize)
        {
            if(pagesize < 1 || pagesize > 100)
            {
                return false;
            }

            Repository.Models.Setting setting = new Repository.Models.Setting();
            setting.Setting1 = "reviewspagesize";
            setting.IntValue = pagesize;
            return await _repo.SetSetting(setting);
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

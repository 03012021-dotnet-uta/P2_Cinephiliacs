using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GlobalModels;

namespace CineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieLogic _movieLogic;
        public MovieController(IMovieLogic movieLogic)
        {
            _movieLogic = movieLogic;
        }
        
        /// <summary>
        /// Returns a list of all Review objects for the specified movie ID.
        /// </summary>
        /// <param name="movieid"></param>
        /// <returns></returns>
        [HttpGet("reviews/{movieid}")]
        public async Task<ActionResult<List<Review>>> GetReviews(string movieid)
        {
            List<Review> reviews = await _movieLogic.GetReviews(movieid);

            if(reviews == null)
            {
                return StatusCode(404);
            }
            StatusCode(200);
            return reviews;
        }

        /// <summary>
        /// Adds a new Movie Review based on the information provided.
        /// Returns a 400 status code if creation fails.
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        [HttpPost("review")]
        public async Task<ActionResult> CreateReview([FromBody] Review review)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            if(await _movieLogic.CreateReview(review))
            {
                return StatusCode(201);
            }
            else
            {
                return StatusCode(400);
            }
        }

        /// <summary>
        /// Adds a new Movie based on the information provided.
        /// Returns a 400 status code if creation fails.
        /// </summary>
        /// <param name="movieid"></param>
        /// <returns></returns>
        [HttpPost("{movieid}")]
        public async Task<ActionResult> CreateMovie(string movieid)
        {
            if (await _movieLogic.CreateMovie(movieid))
            {
                return StatusCode(201);
            }
            else
            {
                return StatusCode(400);
            }
        }

    }
}

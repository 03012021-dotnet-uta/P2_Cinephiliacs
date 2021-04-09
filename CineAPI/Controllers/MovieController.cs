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
        
        [HttpGet("reviews/{movieid}")]
        public ActionResult<List<Review>> GetReviews(string movieid)
        {
            List<Review> reviews = _movieLogic.GetReviews(movieid);

            if(reviews == null)
            {
                return StatusCode(404);
            }
            StatusCode(200);
            return reviews;
        }

        [HttpPost("review")]
        public ActionResult NewReview([FromBody] Review review)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            if(_movieLogic.NewReview(review))
            {
                return StatusCode(201);
            }
            else
            {
                return StatusCode(400);
            }
        }

        [HttpPost("{movieid}")]
        public ActionResult NewMovie(string movieid)
        {
            if (_movieLogic.NewMovie(movieid))
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

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
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpGet("users")]
        public ActionResult<List<User>> Get()
        {
            List<User> users = _userLogic.GetUsers();

            if(users == null)
            {
                return StatusCode(404);
            }
            StatusCode(200);
            return users;
        }

        [HttpPost]
        public ActionResult CreateUser([FromBody] User user)
        {            
            if(!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            if(_userLogic.CreateUser(user))
            {
                return StatusCode(201);
            }
            else
            {
                return StatusCode(400);
            }
        }

        [HttpGet("{username}")]
        public ActionResult<User> GetUser(string username)
        {
            return _userLogic.GetUser(username);
        }

        [HttpGet("discussions/{username}")]
        public ActionResult<List<Discussion>> GetDiscussions(string username)
        {
            List<Discussion> discussions = _userLogic.GetDiscussions(username);

            if(discussions == null)
            {
                return StatusCode(404);
            }
            StatusCode(200);
            return discussions;
        }

        [HttpGet("comments/{username}")]
        public ActionResult<List<Comment>> GetComments(string username)
        {
            List<Comment> comments = _userLogic.GetComments(username);

            if(comments == null)
            {
                return StatusCode(404);
            }
            StatusCode(200);
            return comments;
        }

        [HttpGet("movies/{username}")]
        public ActionResult<List<string>> GetFollowingMovies(string username)
        {
            List<string> movieids = _userLogic.GetFollowingMovies(username);

            if(movieids == null)
            {
                return StatusCode(404);
            }
            StatusCode(200);
            return movieids;
        }

        [HttpGet("reviews/{username}")]
        public ActionResult<List<Review>> GetReviews(string username)
        {
            List<Review> reviews = _userLogic.GetReviews(username);

            if(reviews == null)
            {
                return StatusCode(404);
            }
            StatusCode(200);
            return reviews;
        }

        [HttpPost("movie/{username}/{movieid}")]
        public ActionResult FollowMovie(string username, string movieid)
        {
            if(_userLogic.FollowMovie(username, movieid))
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

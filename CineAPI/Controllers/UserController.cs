using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Returns a list of User objects that includes every User.
        /// </summary>
        /// <returns></returns>
        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> Get()
        {
            return await _userLogic.GetUsers();
        }

        /// <summary>
        /// Adds a new User based on the information provided.
        /// Returns a 400 status code if creation fails.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] User user)
        {            
            if(!ModelState.IsValid)
            {
                Console.WriteLine("UserController.CreateUser() was called with invalid body data.");
                return StatusCode(400);
            }

            if(await _userLogic.CreateUser(user))
            {
                return StatusCode(201);
            }
            else
            {
                return StatusCode(200);
            }
        }

        /// <summary>
        /// Returns the User information associated with the provided username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("{username}")]
        public ActionResult<User> GetUser(string username)
        {
            User user = _userLogic.GetUser(username);

            if(user == null)
            {
                return StatusCode(404);
            }
            StatusCode(200);
            return user;
        }

        /// <summary>
        /// Returns a list of all Discussion objects that were created by the User
        /// with the provided username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("discussions/{username}")]
        public async Task<ActionResult<List<Discussion>>> GetDiscussions(string username)
        {
            List<Discussion> discussions = await _userLogic.GetDiscussions(username);
            
            if(discussions == null)
            {
                return StatusCode(404);
            }
            if(discussions.Count == 0)
            {
                return StatusCode(204);
            }
            StatusCode(200);
            return discussions;
        }

        /// <summary>
        /// Returns a list of all Comment objects that were created by the User
        /// with the provided username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("comments/{username}")]
        public async Task<ActionResult<List<Comment>>> GetComments(string username)
        {
            List<Comment> comments = await _userLogic.GetComments(username);
            
            if(comments == null)
            {
                return StatusCode(404);
            }
            if(comments.Count == 0)
            {
                return StatusCode(204);
            }
            StatusCode(200);
            return comments;
        }

        /// <summary>
        /// Returns a list containing all of the movie IDs for the Movies that
        /// the User with the provided username is following.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("movies/{username}")]
        public async Task<ActionResult<List<string>>> GetFollowingMovies(string username)
        {
            List<string> movieids = await _userLogic.GetFollowingMovies(username);
            
            if(movieids == null)
            {
                return StatusCode(404);
            }
            if(movieids.Count == 0)
            {
                return StatusCode(204);
            }
            StatusCode(200);
            return movieids;
        }

        /// <summary>
        /// Returns a list of all Review objects that were created by the User
        /// with the provided username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("reviews/{username}")]
        public async Task<ActionResult<List<Review>>> GetReviews(string username)
        {
            List<Review> reviews = await _userLogic.GetReviews(username);

            if(reviews == null)
            {
                return StatusCode(404);
            }
            if(reviews.Count == 0)
            {
                return StatusCode(204);
            }
            StatusCode(200);
            return reviews;
        }

        /// <summary>
        /// Adds the Movie with the provided movie ID to the provided User's 
        /// list of Movies they are following.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="movieid"></param>
        /// <returns></returns>
        [HttpPost("movie/{username}/{movieid}")]
        public async Task<ActionResult> FollowMovie(string username, string movieid)
        {
            var result = await _userLogic.FollowMovie(username, movieid);
            if(result)
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

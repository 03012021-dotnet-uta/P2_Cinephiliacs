using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using GlobalModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserMethods _userMethods;
        public UserController(UserMethods userMethods)
        {
            _userMethods = userMethods;
        }

        /// <summary>
        /// Returns the information associated with a username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("user/{username}")]
        public ActionResult<User> GetUser(string username)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400);
            }
            User user = _userMethods.GetUser(username);

            if(user == null)
            {
                return StatusCode(400);
            }
            if(user.Username == "")
            {
                return StatusCode(404);
            }
            StatusCode(202);
            return user;
        }

        /// <summary>
        /// Creates a new User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("user")]
        public ActionResult CreateUser([FromBody] User user)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            if(_userMethods.CreateUser(user))
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

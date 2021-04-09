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
            if(!ModelState.IsValid)
            {
                return StatusCode(400);
            }

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
            if(!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            return _userLogic.GetUser(username);
        }
    }
}

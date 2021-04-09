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
    public class ForumController : ControllerBase
    {
        private readonly IForumLogic _forumLogic;
        public ForumController(IForumLogic forumLogic)
        {
            _forumLogic = forumLogic;
        }

        [HttpGet("topics")]
        public ActionResult<List<string>> GetTopics()
        {
            List<string> topics = _forumLogic.GetTopics();

            if(topics == null)
            {
                return StatusCode(404);
            }
            StatusCode(200);
            return topics;
        }

        [HttpGet("discussions/{movieid}")]
        public ActionResult<List<Discussion>> GetDiscussions(string movieid)
        {
            List<Discussion> discussions = _forumLogic.GetDiscussions(movieid);

            if(discussions == null)
            {
                return StatusCode(404);
            }
            StatusCode(200);
            return discussions;
        }

        [HttpGet("comments/{discussionid}")]
        public ActionResult<List<Comment>> GetComments(int discussionid)
        {
            List<Comment> comments = _forumLogic.GetComments(discussionid);

            if(comments == null)
            {
                return StatusCode(404);
            }
            StatusCode(200);
            return comments;
        }

        [HttpPost("discussion")]
        public ActionResult CreateDiscussion([FromBody] NewDiscussion discussion)
        {            
            if(!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            if(_forumLogic.CreateDiscussion(discussion))
            {
                return StatusCode(201);
            }
            else
            {
                return StatusCode(400);
            }
        }

        [HttpPost("comment")]
        public ActionResult CreateComment([FromBody] NewComment comment)
        {            
            if(!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            if(_forumLogic.CreateComment(comment))
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

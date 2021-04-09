using System;
using System.Collections.Generic;
using GlobalModels;
using Repository;

namespace BusinessLogic
{
    public class ForumLogic : Interfaces.IForumLogic
    {
        private readonly RepoLogic _repo;
        
        public ForumLogic(RepoLogic repo)
        {
            _repo = repo;
        }

    }
}

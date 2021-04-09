using System;
using System.Collections.Generic;
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

    }
}

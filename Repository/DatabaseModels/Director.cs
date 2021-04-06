using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DatabaseModels
{
    public partial class Director
    {
        public Director()
        {
            MovieDirectors = new HashSet<MovieDirector>();
        }

        public string DirectorName { get; set; }

        public virtual ICollection<MovieDirector> MovieDirectors { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DatabaseModels
{
    public partial class Actor
    {
        public Actor()
        {
            MovieActors = new HashSet<MovieActor>();
        }

        public string ActorName { get; set; }

        public virtual ICollection<MovieActor> MovieActors { get; set; }
    }
}

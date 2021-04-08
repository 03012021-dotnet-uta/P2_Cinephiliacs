using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class MovieActor
    {
        public string MovieId { get; set; }
        public string ActorName { get; set; }

        public virtual Actor ActorNameNavigation { get; set; }
        public virtual Movie Movie { get; set; }
    }
}

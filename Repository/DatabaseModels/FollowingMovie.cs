using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DatabaseModels
{
    public partial class FollowingMovie
    {
        public string Username { get; set; }
        public string MovieId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual User UsernameNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Tag
    {
        public Tag()
        {
            BlockedTags = new HashSet<BlockedTag>();
            MovieTags = new HashSet<MovieTag>();
        }

        public string TagName { get; set; }
        public bool IsBanned { get; set; }

        public virtual ICollection<BlockedTag> BlockedTags { get; set; }
        public virtual ICollection<MovieTag> MovieTags { get; set; }
    }
}

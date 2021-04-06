using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DatabaseModels
{
    public partial class MovieTag
    {
        public string MovieId { get; set; }
        public string TagName { get; set; }
        public string Username { get; set; }
        public bool? IsUpvote { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Tag TagNameNavigation { get; set; }
        public virtual User UsernameNavigation { get; set; }
    }
}

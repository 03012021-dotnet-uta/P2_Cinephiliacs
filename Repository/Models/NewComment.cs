using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class NewComment
    {
        public string Username { get; set; }
        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual User UsernameNavigation { get; set; }
    }
}

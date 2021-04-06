using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DatabaseModels
{
    public partial class Comment
    {
        public Comment()
        {
            NewComments = new HashSet<NewComment>();
        }

        public int CommentId { get; set; }
        public int DiscussionId { get; set; }
        public string Username { get; set; }
        public DateTime CreationTime { get; set; }
        public string CommentText { get; set; }
        public bool IsSpoiler { get; set; }

        public virtual Discussion Discussion { get; set; }
        public virtual User UsernameNavigation { get; set; }
        public virtual ICollection<NewComment> NewComments { get; set; }
    }
}

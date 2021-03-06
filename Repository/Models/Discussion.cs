using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Discussion
    {
        public Discussion()
        {
            Comments = new HashSet<Comment>();
            DiscussionTopics = new HashSet<DiscussionTopic>();
        }

        public int DiscussionId { get; set; }
        public string MovieId { get; set; }
        public string Username { get; set; }
        public DateTime CreationTime { get; set; }
        public string Subject { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual User UsernameNavigation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<DiscussionTopic> DiscussionTopics { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DatabaseModels
{
    public partial class Review
    {
        public string Username { get; set; }
        public string MovieId { get; set; }
        public decimal Rating { get; set; }
        public string Review1 { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual User UsernameNavigation { get; set; }
    }
}

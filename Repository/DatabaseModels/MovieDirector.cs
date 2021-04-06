using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DatabaseModels
{
    public partial class MovieDirector
    {
        public string MovieId { get; set; }
        public string DirectorName { get; set; }

        public virtual Director DirectorNameNavigation { get; set; }
        public virtual Movie Movie { get; set; }
    }
}

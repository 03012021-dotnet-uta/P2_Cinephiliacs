using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DatabaseModels
{
    public partial class MovieGenre
    {
        public string MovieId { get; set; }
        public string GenreName { get; set; }

        public virtual Genre GenreNameNavigation { get; set; }
        public virtual Movie Movie { get; set; }
    }
}

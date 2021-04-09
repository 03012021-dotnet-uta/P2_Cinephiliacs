using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Discussions = new HashSet<Discussion>();
            FollowingMovies = new HashSet<FollowingMovie>();
            MovieTags = new HashSet<MovieTag>();
            Reviews = new HashSet<Review>();
        }

        public string MovieId { get; set; }

        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<FollowingMovie> FollowingMovies { get; set; }
        public virtual ICollection<MovieTag> MovieTags { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}

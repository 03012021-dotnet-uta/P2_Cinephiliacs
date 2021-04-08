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
            MovieActors = new HashSet<MovieActor>();
            MovieDirectors = new HashSet<MovieDirector>();
            MovieGenres = new HashSet<MovieGenre>();
            MovieLanguages = new HashSet<MovieLanguage>();
            MovieTags = new HashSet<MovieTag>();
            Reviews = new HashSet<Review>();
        }

        public string MovieId { get; set; }
        public string Title { get; set; }
        public decimal? Year { get; set; }
        public string Rated { get; set; }
        public DateTime? Released { get; set; }
        public decimal? RuntimeMinutes { get; set; }
        public string Plot { get; set; }
        public string Awards { get; set; }
        public string Country { get; set; }
        public string PosterLink { get; set; }

        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<FollowingMovie> FollowingMovies { get; set; }
        public virtual ICollection<MovieActor> MovieActors { get; set; }
        public virtual ICollection<MovieDirector> MovieDirectors { get; set; }
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
        public virtual ICollection<MovieLanguage> MovieLanguages { get; set; }
        public virtual ICollection<MovieTag> MovieTags { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class User
    {
        public User()
        {
            BlockedTags = new HashSet<BlockedTag>();
            Comments = new HashSet<Comment>();
            Discussions = new HashSet<Discussion>();
            FollowingMovies = new HashSet<FollowingMovie>();
            FollowingUserFolloweeNavigations = new HashSet<FollowingUser>();
            FollowingUserFollowerNavigations = new HashSet<FollowingUser>();
            MovieTags = new HashSet<MovieTag>();
            NewComments = new HashSet<NewComment>();
            Reviews = new HashSet<Review>();
        }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte Permissions { get; set; }

        public virtual ICollection<BlockedTag> BlockedTags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<FollowingMovie> FollowingMovies { get; set; }
        public virtual ICollection<FollowingUser> FollowingUserFolloweeNavigations { get; set; }
        public virtual ICollection<FollowingUser> FollowingUserFollowerNavigations { get; set; }
        public virtual ICollection<MovieTag> MovieTags { get; set; }
        public virtual ICollection<NewComment> NewComments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}

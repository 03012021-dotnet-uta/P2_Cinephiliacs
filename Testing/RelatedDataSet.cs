using System;

namespace Testing
{
    internal class RelatedDataSet
    {
        static int id = 1;
        internal Repository.Models.User User { get; }
        internal Repository.Models.Movie Movie { get; }
        internal Repository.Models.Review Review { get; }
        internal Repository.Models.Topic Topic { get; }
        internal Repository.Models.Discussion Discussion { get; }
        internal Repository.Models.Comment Comment { get; }
        internal Repository.Models.DiscussionTopic DiscussionTopic { get; }
        internal Repository.Models.FollowingMovie FollowingMovie { get; }

        internal RelatedDataSet(string username, string movieId, string topicName)
        {
            User = new Repository.Models.User();
            User.Username = username;
            User.FirstName = "Jimmy";
            User.LastName = "Jimerson";
            User.Email = "JimmyJimerson@gmail.com";
            User.Permissions = 1;
            
            Movie = new Repository.Models.Movie();
            Movie.MovieId = movieId;

            Review = new Repository.Models.Review();
            Review.MovieId = movieId;
            Review.Username = username;
            Review.Rating = 4.0m;
            Review.CreationTime = DateTime.Now;
            
            Topic = new Repository.Models.Topic();
            Topic.TopicName = movieId;

            Discussion = new Repository.Models.Discussion();
            Discussion.DiscussionId = id;
            Discussion.MovieId = movieId;
            Discussion.Username = username;
            Discussion.CreationTime = DateTime.Now;
            Discussion.Subject = "This movie doesn't exist.";

            Comment = new Repository.Models.Comment();
            Comment.CommentId = id;
            Comment.DiscussionId = id;
            Comment.Username = username;
            Comment.CreationTime = DateTime.Now;
            Comment.CommentText = "This movie is UNREAL!";
            Comment.IsSpoiler = true;

            DiscussionTopic = new Repository.Models.DiscussionTopic();
            DiscussionTopic.DiscussionId = id;
            DiscussionTopic.TopicName = topicName;

            FollowingMovie = new Repository.Models.FollowingMovie();
            FollowingMovie.Username = username;
            FollowingMovie.MovieId = movieId;

            id += 1;
        }

    }
}

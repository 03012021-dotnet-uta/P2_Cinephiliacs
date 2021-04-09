using System;
using System.Collections.Generic;
using GlobalModels;

namespace BusinessLogic
{
    public static class UserMapper
    {
        /// <summary>
        /// Maps an instance of GlobalModels.User onto Repository.Models.User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Repository.Models.User UserToRepoUser(User user)
        {
            var repoUser = new Repository.Models.User();
            repoUser.Username = user.Username;
            repoUser.FirstName = user.Firstname;
            repoUser.LastName = user.Lastname;
            repoUser.Email = user.Email;
            repoUser.Permissions = user.Permissions;

            return repoUser;
        }

        public static User RepoUserToUser(Repository.Models.User repoUser)
        {
            var user = new User(repoUser.Username, repoUser.FirstName, repoUser.LastName, repoUser.Email, repoUser.Permissions);
            return user;
        }

        internal static Discussion RepoDiscussionToDiscussion(Repository.Models.Discussion repoDiscussion, Repository.Models.Topic topic)
        {
            var discussion = new Discussion(repoDiscussion.MovieId, repoDiscussion.Username, repoDiscussion.Subject, topic.TopicName);
            return discussion;
        }

        internal static Comment RepoCommentToComment(Repository.Models.Comment repoComment)
        {
            var comment = new Comment(repoComment.DiscussionId, repoComment.Username, repoComment.CommentText, repoComment.IsSpoiler);
            return comment;
        }

        internal static Review RepoReviewToReview(Repository.Models.Review repoReview)
        {
            var review = new Review(repoReview.MovieId, repoReview.Username, repoReview.Rating, repoReview.Review1);
            return review;
        }
    }
}

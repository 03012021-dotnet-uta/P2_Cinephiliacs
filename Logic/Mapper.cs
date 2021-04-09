using System;
using System.Collections.Generic;
using GlobalModels;

namespace BusinessLogic
{
    internal static class Mapper
    {
        /// <summary>
        /// Maps an instance of GlobalModels.User onto Repository.Models.User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        internal static Repository.Models.User UserToRepoUser(User user)
        {
            var repoUser = new Repository.Models.User();
            repoUser.Username = user.Username;
            repoUser.FirstName = user.Firstname;
            repoUser.LastName = user.Lastname;
            repoUser.Email = user.Email;
            repoUser.Permissions = user.Permissions;

            return repoUser;
        }

        internal static User RepoUserToUser(Repository.Models.User repoUser)
        {
            var user = new User(repoUser.Username, repoUser.FirstName, repoUser.LastName, repoUser.Email, repoUser.Permissions);
            return user;
        }

        internal static Discussion RepoDiscussionToDiscussion(Repository.Models.Discussion repoDiscussion, Repository.Models.Topic topic)
        {
            var discussion = new Discussion(repoDiscussion.DiscussionId, repoDiscussion.MovieId, repoDiscussion.Username, repoDiscussion.Subject, topic.TopicName);
            return discussion;
        }

        internal static Comment RepoCommentToComment(Repository.Models.Comment repoComment)
        {
            var comment = new Comment(repoComment.CommentId, repoComment.DiscussionId, repoComment.Username, repoComment.CommentText, repoComment.IsSpoiler);
            return comment;
        }

        internal static Review RepoReviewToReview(Repository.Models.Review repoReview)
        {
            var review = new Review(repoReview.MovieId, repoReview.Username, repoReview.Rating, repoReview.Review1);
            return review;
        }

        internal static Repository.Models.Review ReviewToRepoReview(Review review)
        {
            var repoReview = new Repository.Models.Review();
            repoReview.Username = review.Username;
            repoReview.MovieId = review.Movieid;
            repoReview.Rating = review.Rating;
            repoReview.CreationTime = DateTime.Now;

            return repoReview;
        }

        internal static Repository.Models.Discussion DiscussionToNewRepoDiscussion(NewDiscussion discussion)
        {
            var repoDiscussion = new Repository.Models.Discussion();
            repoDiscussion.MovieId = discussion.Movieid;
            repoDiscussion.Username = discussion.Username;
            repoDiscussion.Subject = discussion.Subject;
            repoDiscussion.CreationTime = DateTime.Now;

            return repoDiscussion;
        }

        internal static Repository.Models.Comment DiscussionToNewRepoComment(NewComment comment)
        {
            var repoComment = new Repository.Models.Comment();
            repoComment.DiscussionId = comment.Discussionid;
            repoComment.Username = comment.Username;
            repoComment.CommentText = comment.Text;
            repoComment.CreationTime = DateTime.Now;
            repoComment.IsSpoiler = comment.Isspoiler;

            return repoComment;
        }
    }
}

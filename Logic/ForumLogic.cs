using System;
using System.Collections.Generic;
using GlobalModels;
using Repository;

namespace BusinessLogic
{
    public class ForumLogic : Interfaces.IForumLogic
    {
        private readonly RepoLogic _repo;
        
        public ForumLogic(RepoLogic repo)
        {
            _repo = repo;
        }

        public bool CreateComment(NewComment comment)
        {
            var repoComment = Mapper.DiscussionToNewRepoComment(comment);
            return _repo.AddComment(repoComment);
        }

        public bool CreateDiscussion(NewDiscussion discussion)
        {
            var repoDiscussion = Mapper.DiscussionToNewRepoDiscussion(discussion);
            return _repo.AddDiscussion(repoDiscussion);
        }

        public List<Comment> GetComments(int discussionid)
        {
            List<Repository.Models.Comment> repoComments = _repo.GetMovieComments(discussionid);
            if(repoComments == null)
            {
                return null;
            }

            List<Comment> comments = new List<Comment>();
            foreach (var repoComment in repoComments)
            {
                comments.Add(Mapper.RepoCommentToComment(repoComment));
            }
            return comments;
        }

        public List<Discussion> GetDiscussions(string movieid)
        {
            List<Repository.Models.Discussion> repoDiscussions = _repo.GetMovieDiscussions(movieid);
            if(repoDiscussions == null)
            {
                return null;
            }

            List<Discussion> discussions = new List<Discussion>();
            foreach (var repoDiscussion in repoDiscussions)
            {
                // Get the topic associated with this discussion
                Repository.Models.Topic topic = _repo.GetDiscussionTopic(repoDiscussion.DiscussionId);
                if(topic == null)
                {
                    topic = new Repository.Models.Topic();
                    topic.TopicName = "None";
                }
                discussions.Add(Mapper.RepoDiscussionToDiscussion(repoDiscussion, topic));
            }
            return discussions;
        }

        public List<string> GetTopics()
        {
            var repoTopics = _repo.GetTopics();
            if(repoTopics == null)
            {
                return null;
            }

            var topics = new List<string>();
            foreach (var repoTopic in repoTopics)
            {
                topics.Add(repoTopic.TopicName);
            }
            return topics;
        }
    }
}

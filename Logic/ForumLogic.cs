using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<bool> CreateComment(NewComment comment)
        {
            var repoComment = Mapper.NewCommentToNewRepoComment(comment);
            return await _repo.AddComment(repoComment);
        }

        public async Task<bool> CreateDiscussion(NewDiscussion discussion)
        {
            var repoDiscussion = Mapper.NewDiscussionToNewRepoDiscussion(discussion);
            return await _repo.AddDiscussion(repoDiscussion);
        }

        public async Task<List<Comment>> GetComments(int discussionid)
        {
            List<Repository.Models.Comment> repoComments = await _repo.GetMovieComments(discussionid);
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

        public async Task<List<Discussion>> GetDiscussions(string movieid)
        {
            List<Repository.Models.Discussion> repoDiscussions = await _repo.GetMovieDiscussions(movieid);
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

        public async Task<List<string>> GetTopics()
        {
            var repoTopics = await _repo.GetTopics();
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

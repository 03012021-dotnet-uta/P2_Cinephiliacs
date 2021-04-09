using System.Collections.Generic;
using GlobalModels;

namespace BusinessLogic.Interfaces
{
    public interface IForumLogic
    {
        List<string> GetTopics();
        List<Discussion> GetDiscussions(string movieid);
        List<Comment> GetComments(int discussionid);
        bool CreateDiscussion(NewDiscussion discussion);
        bool CreateComment(NewComment comment);
    }
}
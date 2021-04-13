using System.Collections.Generic;
using System.Threading.Tasks;
using GlobalModels;

namespace BusinessLogic.Interfaces
{
    public interface IForumLogic
    {
        /// <summary>
        /// Returns a list of every topic's name.
        /// </summary>
        /// <returns></returns>
        public Task<List<string>> GetTopics();

        /// <summary>
        /// Returns a list of every Discussion object whose Movieid is equal to
        /// the movieid argument.
        /// </summary>
        /// <param name="movieid"></param>
        /// <returns></returns>
        public Task<List<Discussion>> GetDiscussions(string movieid);

        /// <summary>
        /// Returns a list of every Comment object whose Discussionid is equal to
        /// the discussionid argument.
        /// </summary>
        /// <param name="discussionid"></param>
        /// <returns></returns>
        public Task<List<Comment>> GetComments(int discussionid);

        /// <summary>
        /// Returns Comments objects [n*(page-1), n*(page-1) + n] whose Discussionid
        /// is equal to the discussionid argument. Where n is the current page size
        /// for comments
        /// </summary>
        /// <param name="discussionid"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public Task<List<Comment>> GetCommentsPage(int discussionid, int page);

        /// <summary>
        /// Sets the page size for comments.
        /// </summary>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public Task<bool> SetCommentsPageSize(int pagesize);

        /// <summary>
        /// Adds a new Discussion Object to storage.
        /// Returns true if sucessful; false otherwise.
        /// </summary>
        /// <param name="discussion"></param>
        /// <returns></returns>
        public Task<bool> CreateDiscussion(NewDiscussion discussion);

        /// <summary>
        /// Adds a new Comment Object to storage.
        /// Returns true if sucessful; false otherwise.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public Task<bool> CreateComment(NewComment comment);
    }
}
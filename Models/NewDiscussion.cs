using System;

namespace GlobalModels
{
    public sealed class NewDiscussion
    {
        public string Movieid { get; set; }
        public string Username { get; set; }
        public string Subject { get; set; }
        public string Topic { get; set; }

        public NewDiscussion(string movieid, string username, string subject, string topic)
        {
            Movieid = movieid;
            Username = username;
            Subject = subject;
            Topic = topic;
        }
        public NewDiscussion(Discussion discussion)
        {
            Movieid = discussion.Movieid;
            Username = discussion.Username;
            Subject = discussion.Subject;
            Topic = discussion.Topic;
        }
    }
}
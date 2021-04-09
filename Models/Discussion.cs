using System;

namespace GlobalModels
{
    public class Discussion
    {
        public int Discussionid { get; set; }
        public string Movieid { get; set; }
        public string Username { get; set; }
        public string Subject { get; set; }
        public string Topic { get; set; }

        public Discussion(int discussionid, string movieid, string username, string subject, string topic)
        {
            Discussionid = discussionid;
            Movieid = movieid;
            Username = username;
            Subject = subject;
            Topic = topic;
        }
    }
}
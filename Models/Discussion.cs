using System;

namespace GlobalModels
{
    public class Discussion
    {
        public string Movieid { get; set; }
        public string Username { get; set; }
        public string Subject { get; set; }
        public string Topic { get; set; }

        public Discussion(string movieid, string username, string subject, string topic)
        {
            Movieid = movieid;
            Username = username;
            Subject = subject;
            Topic = topic;
        }
    }
}
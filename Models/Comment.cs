using System;

namespace GlobalModels
{
    public class Comment
    {
        public int Discussionid { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public bool Isspoiler { get; set; }

        public Comment(int discussionid, string username, string text, bool isspoiler)
        {
            Discussionid = discussionid;
            Username = username;
            Text = text;
            Isspoiler = isspoiler;
        }
    }
}
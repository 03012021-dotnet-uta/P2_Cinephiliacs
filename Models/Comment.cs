using System;

namespace GlobalModels
{
    public class Comment
    {
        public int Commentid { get; set; }
        public int Discussionid { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public bool Isspoiler { get; set; }

        public Comment(int commentid, int discussionid, string username, string text, bool isspoiler)
        {
            Commentid = commentid;
            Discussionid = discussionid;
            Username = username;
            Text = text;
            Isspoiler = isspoiler;
        }
    }
}
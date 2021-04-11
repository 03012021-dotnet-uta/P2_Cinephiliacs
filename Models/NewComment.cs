using System;

namespace GlobalModels
{
    public class NewComment
    {
        public int Discussionid { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public bool Isspoiler { get; set; }

        public NewComment(int discussionid, string username, string text, bool isspoiler)
        {
            Discussionid = discussionid;
            Username = username;
            Text = text;
            Isspoiler = isspoiler;
        }
        public NewComment(Comment comment)
        {
            Discussionid = comment.Discussionid;
            Username = comment.Username;
            Text = comment.Text;
            Isspoiler = comment.Isspoiler;
        }
    }
}
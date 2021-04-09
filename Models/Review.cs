using System;

namespace GlobalModels
{
    public class Review
    {
        public string Movieid { get; set; }
        public string Username { get; set; }
        public decimal Rating { get; set; }
        public string Text { get; set; }

        public Review(string movieid, string username, decimal rating, string text)
        {
            Movieid = movieid;
            Username = username;
            Rating = rating;
            Text = text;
        }
    }
}
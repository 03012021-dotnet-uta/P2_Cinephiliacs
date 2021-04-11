using System;

namespace GlobalModels
{
    public sealed class Review : IEquatable<Review>
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

        public bool Equals(Review obj)
        {
            if (Object.ReferenceEquals(obj, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return Movieid == obj.Movieid;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Review);
        }

        public static bool operator ==(Review lhs, Review rhs)
        {
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    return true;
                }

                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Review lhs, Review rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode()
        {
            return Movieid.GetHashCode();
        }
    }
}
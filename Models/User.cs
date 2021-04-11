using System;

namespace GlobalModels
{
    public sealed class User : IEquatable<User>
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public byte Permissions { get; set; }

        public User(string username, string firstname, string lastname, string email, byte permissions)
        {
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Permissions = permissions;
        }

        public bool Equals(User other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            return Username == other.Username;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as User);
        }

        public static bool operator ==(User lhs, User rhs)
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

        public static bool operator !=(User lhs, User rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode();
        }
    }
}

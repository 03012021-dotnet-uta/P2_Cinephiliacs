using System;

namespace GlobalModels
{
    public class User
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public byte Permissions { get; set; }

        public User(string username, string firstname, string lastname, string email, byte permissions)
        {
            this.Username = username;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Email = email;
            this.Permissions = permissions;
        }
    }
}

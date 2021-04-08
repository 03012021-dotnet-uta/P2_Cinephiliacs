using GlobalModels;

namespace BusinessLogic.Interfaces
{
    public interface IUserLogic
    {
        public bool CreateUser(User user);

        public User GetUser(string username);
    }
}
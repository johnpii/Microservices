using SharedLibrary.Models;

namespace Auth.Interfaces.Services
{
    public interface IUserService
    {
        bool ExistByEmail(string email);
        void AddUser(User user);
        Task<User> FindByEmailAndPassWord(string email, string password);
    }
}

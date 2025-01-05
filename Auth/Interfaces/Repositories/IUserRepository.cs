using SharedLibrary.Models;

namespace Auth.Interfaces.Repositories
{
    public interface IUserRepository
    {
        bool ExistByEmail(string email);
        void AddUser(User user);
        Task<User> FindByEmailAndPassWord(string email, string password);
    }
}

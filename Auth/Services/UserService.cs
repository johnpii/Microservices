using Auth.Interfaces.Repositories;
using Auth.Interfaces.Services;
using SharedLibrary.Models;

namespace Auth.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public void AddUser(User user)
        {
            _userRepo.AddUser(user);
        }

        public bool ExistByEmail(string email)
        {
            return _userRepo.ExistByEmail(email);
        }

        public async Task<User> FindByEmailAndPassWord(string email, string password)
        {
            return await _userRepo.FindByEmailAndPassWord(email, password);
        }
    }
}

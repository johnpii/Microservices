using Auth.Data;
using Auth.Interfaces.Repositories;
using Auth.Utilities;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace Auth.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ExistByEmail(string email)
        {
            return _context.Users.Any(p => p.Email == email);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task<User> FindByEmailAndPassWord(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Email == email && p.Password == PasswordEncryption.EncryptPassword(password));
        }
    }
}

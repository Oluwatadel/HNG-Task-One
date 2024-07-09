using Microsoft.EntityFrameworkCore;
using User_Registartion.Context;
using User_Registartion.Entity;
using User_Registartion.Repository.Interface;

namespace User_Registartion.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDBContext _context;

        public UserRepository(UserDBContext context)
        {
            _context = context;
        }
        public async Task<User> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            return user;
        }
        
        public async Task<User?> GetUser(string email)
        {
            var user = await _context.Users.Include(a => a.Organisations).FirstOrDefaultAsync(a => a.UserId == email || a.Email == email);
            return user;
        }

        
    }
}

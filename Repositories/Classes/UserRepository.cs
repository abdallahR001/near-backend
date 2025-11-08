using Backend.Data;
using Backend.Data.Models;
using Backend.DTOs;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> Register(User user)
        {
            try
            {
                var newUser = await _dbContext.Users.AddAsync(user);

                return user;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        public async Task Login(LoginRequestDTO request)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if(user != null)
                return true;

            return false;
        }
    }
}
using Backend.Data;
using Backend.Data.Models;
using Backend.DTOs;
using Backend.Helpers;
using Backend.Repositories.Interfaces;
using Backend.Results;
using Microsoft.AspNetCore.Identity;
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
        public async Task<UserRegisterResult> Register(User user)
        {
            var newUser = await _dbContext.Users.
            AddAsync(user);

            return new UserRegisterResult{
                UserId = newUser.Entity.Id,
                Email = newUser.Entity.Email
            };
            
        }
        public async Task<bool> Login(string requestPassword,string password)
        {
            var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(requestPassword,password);

            if(!isPasswordCorrect)
                return false;

            return true;
        }

        public async Task<UserUpdateResult> UpdateProfile(Guid userId,string? bio = null,string? location = null,string? profileImageURL = null,string? coverImageURL = null)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if(user == null)
                return null;

            if(bio != null)
                user.Bio = bio;

            if(location != null)
                user.Location = location;

            if(profileImageURL != null)
                user.Image = profileImageURL;

            if(coverImageURL != null)
                user.CoverImage = coverImageURL;

            return new UserUpdateResult
            {
                UserId = user.Id,
                Name = user.Name,
                Bio = user.Bio,
                Location = user.Location,
                ProfileImageURL = user.Image,
                CoverImageURL = user.CoverImage
            };
        }

        public async Task<bool> DeleteProfile(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if(user == null)
                return false;

            _dbContext.Users.Remove(user);

            return true;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if(user != null)
                return user;

            return null;
        }
    }
}
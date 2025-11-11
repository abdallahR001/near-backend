using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data.Models;
using Backend.DTOs;
using Backend.Helpers;
using Backend.Results;

namespace Backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserRegisterResult> Register(User request);
        Task<bool> Login(string requestPassword,string password);
        Task<UserUpdateResult> UpdateProfile(Guid userId,string? bio,string? location,string? profileImageURL,string? coverImageURL);
        Task<bool> DeleteProfile(Guid userId);
        Task<User> GetUserByEmail(string email);
    }
}
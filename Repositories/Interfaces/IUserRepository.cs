using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data.Models;
using Backend.DTOs;

namespace Backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Register(User request);
        Task Login(LoginRequestDTO request);
        Task<bool> GetUserByEmail(string email);
    }
}
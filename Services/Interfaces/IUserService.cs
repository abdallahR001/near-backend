using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data.Models;
using Backend.DTOs;

namespace Backend.Services
{
    public interface IUserService
    {
        Task<User> Register(RegisterRequestDTO request);
    }
}
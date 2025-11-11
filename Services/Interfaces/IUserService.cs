using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data.Models;
using Backend.DTOs;
using Backend.Helpers;
using Backend.Results;

namespace Backend.Services
{
    public interface IUserService
    {
        Task<Result<UserRegisterResult>> Register(RegisterRequestDTO request);
        Task<Result<UserLoginResult>> Login(LoginRequestDTO request);
        Task<Result<UserUpdateResult>> UpdateProfile(Guid userId,UpdateProfileRequestDTO request);
        Task<Result<bool>> DeleteProfile(Guid userId);
    }
}
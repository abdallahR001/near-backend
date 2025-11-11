using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data.Models;
using Backend.DTOs;
using Backend.Helpers;
using Backend.Repositories.Interfaces;
using Backend.Results;
using Backend.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<UserRegisterResult>> Register(RegisterRequestDTO request)
        {
            if(request.UserName.Length < 6)
            {
                return Result<UserRegisterResult>.Failure("name must be at least 6 characters long");
            }

            if(request.Email.IsNullOrEmpty() || !request.Email.Contains('@'))
            {
                return Result<UserRegisterResult>.Failure("email is not valid");
            }

            var isUserExist = await _unitOfWork.Users.GetUserByEmail(request.Email);

            if(isUserExist != null)
            {
                return Result<UserRegisterResult>.Failure("email is already used");
            }

            if (request.Password.Length < 8)
            {
                return Result<UserRegisterResult>.Failure("password must be at least 8 characters long");
            }

            var newUser = new User
            {
                Name = request.UserName,
                Email = request.Email,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
            };

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password,12);

            newUser.Password = hashedPassword;

            var user = await _unitOfWork.Users.Register(newUser);

            await _unitOfWork.SaveChangesAsync();

            return Result<UserRegisterResult>.Success("created account successfully",
            new UserRegisterResult
            {
                UserId = user.UserId,
                Email = user.Email
            }
            );
        }
        public async Task<Result<UserLoginResult>> Login(LoginRequestDTO request)
        {
            if(request.Email.IsNullOrEmpty() || !request.Email.Contains('@'))
            {
                return Result<UserLoginResult>.Failure("email is not valid");
            }

            var user = await _unitOfWork.Users.GetUserByEmail(request.Email);

            if(user == null)
            {
                return Result<UserLoginResult>.Failure("invalid email or password");
            }

            var isVerified = await _unitOfWork.Users.Login(request.Password,user.Password);

            if(!isVerified)
            {
                return Result<UserLoginResult>.Failure("invalid email or password");
            }

            return Result<UserLoginResult>.Success("logged in successfully",
            new UserLoginResult
            {
                UserId = user.Id,
                Email = user.Email
            }
            );
        }
        public async Task<Result<UserUpdateResult>> UpdateProfile(Guid userId,UpdateProfileRequestDTO request)
        {
            var result = await _unitOfWork.Users.UpdateProfile(userId, request.Bio, request.Location, request.ProfileImageURL, request.CoverImageURL);

            if(result == null)
            {
                return Result<UserUpdateResult>.Failure("user not found");
            }

            await _unitOfWork.SaveChangesAsync();

            return Result<UserUpdateResult>.Success("updated profile successfully",result);
        }

        public async Task<Result<bool>> DeleteProfile(Guid userId)
        {
            var isDeleted = await _unitOfWork.Users.DeleteProfile(userId);

            if(!isDeleted)
            {
                return Result<bool>.Failure("user not found");
            }

            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success("profile deleted successfully",true);
        }
    }
}
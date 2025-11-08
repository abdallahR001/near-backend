using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data.Models;
using Backend.DTOs;
using Backend.Repositories.Interfaces;
using Backend.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }
        public async Task<User> Register(RegisterRequestDTO request)
        {
            try
            {
                if(request.UserName.Length < 6)
                throw new Exception("username must be at least 6 characters long");

            if(request.Email.IsNullOrEmpty() || !request.Email.Contains('@'))
                throw new Exception("email is not valid");

            var isUserExist = await _unitOfWork.Users.GetUserByEmail(request.Email);

            if(isUserExist)
                throw new Exception("user already exist");

            if(request.Password.Length < 8)
                throw new Exception("password must be at least 8 characters long");

            var newUser = new User
            {
                Name = request.UserName,
                Email = request.Email,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, request.Password);

            newUser.Password = hashedPassword;

            var user = await _unitOfWork.Users.Register(newUser);

            await _unitOfWork.SaveChangesAsync();

            return user;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
using System.Security.Claims;
using Backend.DTOs;
using Backend.Helpers;
using Backend.JWT;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Users : ControllerBase
    {
        public IUserService _userService;
        public Users(IUserService userService)
        {
            _userService = userService;
            
        }
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterRequestDTO request)
        {
            var result = await _userService.Register(request);

            if(result.ISuccess)
            {
                var token = JwtHelper.GenerateAccessToken(result.Data.UserId,result.Data.Email);

                Response.Cookies.Append("token", token,
                new CookieOptions{
                    HttpOnly = true,
                });

                return Ok(new
                {
                    message = result.Message,  
                });
            }

            return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            var result = await _userService.Login(request);

            if(result.ISuccess)
            {
                var token = JwtHelper.GenerateAccessToken(result.Data.UserId,result.Data.Email);

                Response.Cookies.Append("token", token);

                return Ok(new
                {
                    message = result.Message,  
                });
            }

            return BadRequest(result.Message);
        }

        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateProfile(Guid userId,UpdateProfileRequestDTO request)
        {
            var result = await _userService.UpdateProfile(userId, request);

            if(result.ISuccess)
            {
                return Ok(new
                {
                    message = result.Message,
                    Data = result.Data,
                });
            }

            return BadRequest(result.Message);
        }
        
        [HttpDelete("")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _userService.DeleteProfile(Guid.Parse(userId));

            if(result.ISuccess)
                return Ok(new
                {
                    message = result.Message
                });

            return BadRequest(result.Message);
        }
    }
}
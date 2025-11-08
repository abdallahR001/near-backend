using Backend.DTOs;
using Backend.JWT;
using Backend.Services;
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
            try
            {
                var user = await _userService.Register(request);

                if(user != null)
                {
                    var token = JwtHelper.GenerateAccessToken(user);

                    return Ok(new
                    {
                        Message = "created account successfully",
                        User = user,
                        AccessToken = token,
                    });
                }

                return BadRequest();
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
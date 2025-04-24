using MangaAPI.Dto;
using MangaAPI.Helpers;
using MangaAPI.Models;
using MangaAPI.Repositories.interfaces;
using MangaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MangaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                var newUser = new User(createUserDTO);
                await _userRepository.CreateUser(newUser);
                return Ok(new { success = true, message = "Create user successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            try
            {
                var user = await _userRepository.Login(login.Username, login.Password.ToSHA256Hash());
                if (user == null)
                {
                    return Unauthorized(new { success = false, message = "Invalid username or password." });
                }

                var token_ = _jwtService.GenerateToken(user.Username);
                return Ok(new { success = true, message = "Login successful", token = token_ });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
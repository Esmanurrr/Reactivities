using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reactivities.API.DTOs;
using Reactivities.API.Services;
using Reactivities.Domain;

namespace Reactivities.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;

        //this controller seperate from others because we will return token in there
        public AccountController(UserManager<AppUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {//to return generic type we use ActionResult
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user == null) return Unauthorized();

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (result)
            {
                return new UserDto
                {
                    Displayame = user.DisplayName,
                    Image = null,
                    Token = _tokenService.CreateToken(user),
                    Username = user.UserName
                };
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registorDto)
        {
            if(await _userManager.Users.AnyAsync(x=>x.UserName == registorDto.Username))
            {
                return BadRequest("Username is already taken");
            }

            if (await _userManager.Users.AnyAsync(x => x.Email == registorDto.Email))
            {
                return BadRequest("Email is already taken");
            }

            var user = new AppUser
            {
                DisplayName = registorDto.DisplayName,
                Email = registorDto.Email,
                UserName = registorDto.Username
            };

            var result = await _userManager.CreateAsync(user, registorDto.Password);

            if (result.Succeeded)
            {
                return new UserDto
                {
                    Displayame = user.DisplayName,
                    Image = null,
                    Token = _tokenService.CreateToken(user),
                    Username = user.UserName
                };
            }

            return BadRequest(result.Errors);

        }
    }
}

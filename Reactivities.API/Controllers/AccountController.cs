using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reactivities.API.DTOs;
using Reactivities.Domain;

namespace Reactivities.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        //this controller seperate from others because we will return token in there
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
                    Token = "this will be a token",
                    Username = user.UserName
                };
            }
            return Unauthorized();
        }
    }
}

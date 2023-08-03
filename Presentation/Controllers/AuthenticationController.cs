using Entities.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Entities.Models;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]

    public class AuthenticationController :ControllerBase
    {
        private readonly IServiceManager _services;
        public AuthenticationController(IServiceManager services)
        {
            _services = services;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var result = await _services.AuthenticationService.RegisterUser(userForRegistrationDto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);

                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _services.AuthenticationService.ValidateUser(user)) //kişi var mı yok mu
            {
                return Unauthorized();//401
            }
            var tokenDto = await _services.AuthenticationService.CreateToken(populateExp: true);
            return Ok(tokenDto);

        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await _services.AuthenticationService.RefreshToken(tokenDto);
            return Ok(tokenDtoToReturn);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut([FromBody] TokenDto tokenDto)
        {
            await _services.AuthenticationService.LogOut(tokenDto.AccessToken);
            return Ok("Çıkış başarılı");


        }



    }
}

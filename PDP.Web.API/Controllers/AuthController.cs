using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PDP.Web.API.Dtos.User;
using PDP.Web.API.Models;
using PDP.Web.API.Services.AuthService;

namespace PDP.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var response = await authService.Login(request);
            return response.Success ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            var response = await authService.Register(request);
            return response.Success ? (IActionResult)Ok(response) : BadRequest(response);
        }

    }
}
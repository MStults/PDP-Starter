using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDP.Web.API.Dtos.User;
using PDP.Web.API.Security;

namespace PDP.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            this.authRepo = authRepo;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var response = await authRepo.Login(request);
            return response.Success ? (IActionResult)Ok(response) : BadRequest(response);
        }
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            var response = await authRepo.Register(request);
            return response.Success ? (IActionResult)Ok(response) : BadRequest(response);
        }

    }
}
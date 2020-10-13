using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDP.Web.API.Dtos.User;
using PDP.Web.API.Security;

namespace PDP.Web.API.Controllers {

    [ApiController]
    [Route ("[controller]")]
    public class TestController : ControllerBase {
        public IActionResult GetString () {
            return Ok (Response<string>.Succeed ("Here is your string"));
        }

        [Authorize]
        [HttpGet ("dashboard")]
        public IActionResult VueDashboard () {
            var data = new object[] {
                new {
                id = 1234,
                title = "Puppy Parade",
                time = "12:00",
                date = "Feb 22, 2022"
                },
                new {
                id = 1584,
                title = "Cat Cabaret",
                time = "9:00",
                date = "March 4, 2022"
                },
                new {
                id = 2794,
                title = "Doggy Day",
                time = "1:00",
                date = "Jun2 12, 2022"
                },
                new {
                id = 4619,
                title = "Feline Frenzy",
                time = "8:00",
                date = "July 18, 2022"
                },
            };

            return Ok (data);
        }
    }
}
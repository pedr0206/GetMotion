using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GetFitness02.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class GreetingController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "Hello " + User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}

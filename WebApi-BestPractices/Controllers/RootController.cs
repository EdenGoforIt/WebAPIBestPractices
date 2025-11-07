using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace WebApi_BestPractices.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController(LinkGenerator linkGenerator) : ControllerBase
    {



    }
}

using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace WebApi_BestPractices.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController(LinkGenerator linkGenerator) : ControllerBase
    {

        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if (mediaType.Contains("application/vnd.apiroot"))
            {
                var list = new List<Link>
                {
                    new() {
                        Href = linkGenerator.GetUriByName(HttpContext, "GetRoot", values: new{ })!,
                        Rel = "self",
                        Method = "GET"
                    },
                     new()
                    {
                        Href = linkGenerator.GetUriByName(HttpContext, "GetCompanies", new{}),
                        Rel = "companies",
                        Method = "GET"
                    },
                    new ()
                    {
                        Href = linkGenerator.GetUriByName(HttpContext, "CreateCompany", new {}),
                        Rel = "create_company",
                        Method = "POST"
                    }
                };

                return Ok(list);
            }

            return NoContent();

        }

    }
}

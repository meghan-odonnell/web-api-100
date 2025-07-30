using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Shows.Api.Shows
{
    public class Controller : ControllerBase
    {
        [HttpPost("/api/shows")]
        public ActionResult AddShow([FromBody] CreateShowRequest request)
        {
            return Ok(request);
        }
    }

    public record CreateShowRequest()
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string StreamingService { get; init; } = string.Empty;
    }



}

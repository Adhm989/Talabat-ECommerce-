using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Errors;

namespace Talabat.Api.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error()
        {
            return NotFound(new ApiResponse(404, "Not Found EndPoint"));
        }
    }
}

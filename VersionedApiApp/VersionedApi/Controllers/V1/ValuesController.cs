using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VersionedApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    public class ValuesController : Controller
    {
        [HttpGet]
        [MapToApiVersion("1.0")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value 2" };
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public IEnumerable<string> GetV2()
        {
            return new string[] { "VERSION 2 value1", "VERSION 2 value 2" };
        }
    }
}

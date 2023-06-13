using Microsoft.AspNetCore.Mvc;

namespace VersionedApi.Controllers.V1;


[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0", Deprecated = true)]
public class UsersController : ControllerBase
{
    // GET: api/UsersController
    //GET: api/v1/users
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "VERSION 1 value1", "VERSION 1 value2" };
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<UsersController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}

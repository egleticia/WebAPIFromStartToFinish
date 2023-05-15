using Microsoft.AspNetCore.Mvc;

namespace GettingStartedApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    // GET: api/Users
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/Users/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }
    // Creates a new record
    // POST api/Users
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }
    // Updates a whole record (or possibly creates)
    // PUT api/Users/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // Updates part of a record
    //PATCH api/Users/5
    [HttpPatch("{id}")]
    public void Patch(int id, [FromBody] string emailAddress)
    {

    }

    // Deletes a record
    // DELETE api/Users/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}

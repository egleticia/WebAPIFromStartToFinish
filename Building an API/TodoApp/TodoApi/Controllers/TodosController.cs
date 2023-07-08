using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoLibrary.DataAcess;
using TodoLibrary.Models;


namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ITodoData _data;

    public TodosController(ITodoData data)
    {
        _data = data;
    }

    private int GetUserId()
    {
        var userIdText = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdText);
    }


    // GET: api/Todos
    [HttpGet]
    public async Task<ActionResult<List<TodoModel>>> Get()
    {
        var result = await _data.GetAllAssigned(GetUserId());

        return Ok(result);
    }

    // GET api/Todos
    [HttpGet("{todoId}")]
    public async Task<ActionResult<TodoModel>> Get(int todoId)
    {
        var result = await _data.GetOneAssigned(GetUserId(), todoId);

        return Ok(result);
    }

    // POST api/Todos
    [HttpPost]
    public async Task<ActionResult<TodoModel>> Post([FromBody] string task)
    {
        var result = await _data.Create(GetUserId(), task);

        return Ok(result);
    }

    // PUT api/Todos/5
    [HttpPut("{todoId}")]
    public async Task<ActionResult> Put(int todoId, [FromBody] string task)
    {
        await _data.UpdateTask(GetUserId(), todoId, task);

        return(Ok());
    }

    // PUT api/Todos/5/Complete
    [HttpPut("{todoId}/Complete")]
    public async Task<IActionResult> Complete(int todoId)
    {
        await _data.CompleteTodo(GetUserId(), todoId);

        return (Ok());
    }

    // DELETE api/Todos
    [HttpDelete("{todoId}")]
    public async Task<IActionResult> Delete(int todoId)
    {
        await _data.DeleteTodo(GetUserId(), todoId);

        return (Ok());
    }



}

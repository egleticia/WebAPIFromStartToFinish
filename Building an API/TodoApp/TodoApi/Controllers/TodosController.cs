using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
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
    private readonly ILogger<TodosController> _logger;

    public TodosController(ITodoData data, ILogger<TodosController> logger)
    {
        _data = data;
        _logger = logger;
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
        _logger.LogInformation("GET: api/Todos");

        try
        {
            var result = await _data.GetAllAssigned(GetUserId());

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The GET call to api/Todos failed");
            return BadRequest();
        }
    }

    // GET api/Todos
    [HttpGet("{todoId}")]
    public async Task<ActionResult<TodoModel>> Get(int todoId)
    {
        _logger.LogInformation($"GET: api/Todos/{todoId}");

        try
        {
            var result = await _data.GetOneAssigned(GetUserId(), todoId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex, 
                "The GET call to {ApiPath} failed. The Id was {todoId}", 
                $"api/Todos/Id",
                todoId);
            return BadRequest();
        }
    }

    // POST api/Todos
    [HttpPost]
    public async Task<ActionResult<TodoModel>> Post([FromBody] string task)
    {
        _logger.LogInformation($"POST: api/Todos (Task: {task})");

        try
        {
            var result = await _data.Create(GetUserId(), task);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"The POST call to api/Todos failed. Task value was {task}");
            return BadRequest();
        }
    }

    // PUT api/Todos/5
    [HttpPut("{todoId}")]
    public async Task<ActionResult> Put(int todoId, [FromBody] string task)
    {
        _logger.LogInformation($"PUT: api/Todos/{todoId} (Task: {task})");

        try
        {
            await _data.UpdateTask(GetUserId(), todoId, task);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"The PUT call to api/Todos/{todoId} failed. Task value was {task}");
            return BadRequest();
        }
    }

    // PUT api/Todos/5/Complete
    [HttpPut("{todoId}/Complete")]
    public async Task<IActionResult> Complete(int todoId)
    {

        _logger.LogInformation($"PUT: api/Todos/{todoId}/Complete");

        try
        {
            await _data.CompleteTodo(GetUserId(), todoId);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"The PUT call to api/Todos/{todoId}/Complete failed");
            return BadRequest();
        }
    }

    // DELETE api/Todos
    [HttpDelete("{todoId}")]
    public async Task<IActionResult> Delete(int todoId)
    {
        _logger.LogInformation($"PUT: api/Todos/{todoId}/Delete");

        try
        {
            await _data.DeleteTodo(GetUserId(), todoId);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"The PUT call to api/Todos/{todoId}/Delete failed");
            return BadRequest();
        }
    }
}

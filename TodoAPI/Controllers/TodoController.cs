using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;
using TodoAPI.Services;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private ITodoService _todoService;

    public TodoController(ITodoService userService)
    {
        _todoService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var todo = await _todoService.GetAll();
        return Ok(todo);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var todo = await _todoService.GetById(id);
        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRequest model)
    {
        await _todoService.Create(model);
        return Ok(new { message = "todo created" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateRequest model)
    {
        await _todoService.Update(id, model);
        return Ok(new { message = "todo updated" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _todoService.Delete(id);
        return Ok(new { message = "todo deleted" });
    }
}
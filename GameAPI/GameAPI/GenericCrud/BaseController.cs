using Microsoft.AspNetCore.Mvc;

namespace GameAPI.GenericCrud;

[Route("[controller]")]
[ApiController]
public class BaseController<T> : ControllerBase where T : class
{
    private readonly IGenericRepository<T> _repository;

    public BaseController(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<T>>> Get([FromQuery] string[]? includes = null)
    {
        var result = await _repository.GetAllAsync(includes);
        
        if (result is not { } validItems)
        {
            var invalidIncludes = ((dynamic)result).InvalidIncludes;
            return BadRequest(new
            {
                Message = "Invalid includes detected.",
                InvalidIncludes = invalidIncludes
            });
        }

        if (!validItems.Any())
            return NotFound();

        return Ok(validItems);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<T>> Get(int id, [FromQuery] string[]? includes = null)
    {
        T? item = await _repository.GetByIdAsync(id, includes);
        
        if (item == null)
        {
            return BadRequest(new
            {
                Message = "Invalid include detected.",
                InvalidIncludes = includes
            });
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<T>> Post([FromBody] T entity)
    {
        await _repository.AddAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = (entity as dynamic).Id }, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] T entity)
    {
        if (id != (entity as dynamic).Id)
            return BadRequest("ID in URL doesn't match the ID in the body.");

        await _repository.UpdateAsync(entity);
        return Ok(new { Message = "Entity successfully updated" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return Ok(new { Message = "Entity successfully deleted" });
    }
}
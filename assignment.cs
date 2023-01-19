using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UpdateController : ControllerBase
{
    private readonly YourDbContext _context;

    public UpdateController(YourDbContext context)
    {
        _context = context;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutYourEntity(int id, YourEntity yourEntity)
    {
        if (id != yourEntity.Id)
        {
            return BadRequest();
        }

        _context.Entry(yourEntity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!YourEntityExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    private bool YourEntityExists(int id)
    {
        return _context.YourEntities.Any(e => e.Id == id);
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParksApi.Models;

namespace ParksApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class StatesController : ControllerBase
  {
    private readonly ParksApiContext _db;

    public StatesController(ParksApiContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<List<State>> Get(string name)
    {      
      IQueryable<State> query = _db.States.AsQueryable();

      if (name != null)
      {
        query = query.Where(entry => entry.Name.Contains(name));
      }      
      return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<State>> GetState(int id)
    {      
      State state = await _db.States
                .Include(s => s.Parks)
                .FirstOrDefaultAsync(s => s.StateId == id);

      if (state == null)
      {
        return NotFound();
      }
      return state;
    }

    [HttpPost]
    public async Task<ActionResult<State>> Post(State state)
    {
      _db.States.Add(state);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetState), new { id = state.StateId }, state);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, State state)
    {
      if (id != state.StateId)
      {
        return BadRequest();
      }

      _db.States.Update(state);

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!StateExists(id))
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

    private bool StateExists(int id)
    {
      return _db.States.Any(e => e.StateId == id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteState(int id)
    {
      State state = await _db.States.FindAsync(id);
      if (state == null)
      {
        return NotFound();
      }

      _db.States.Remove(state);
      await _db.SaveChangesAsync();

      return NoContent();
    }
  }
}
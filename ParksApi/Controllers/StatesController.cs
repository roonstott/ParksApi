using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParksApi.Models;

namespace ParksApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StatesController : ControllerBase
  {
    private readonly ParksApiContext _db;

    public StatesController(ParksApiContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<State>>> Get()
    {
      return await _db.States
                .Include(s => s.Parks)
                .ToListAsync();
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

  }
}
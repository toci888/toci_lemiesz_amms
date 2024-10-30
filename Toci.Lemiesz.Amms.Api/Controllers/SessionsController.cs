using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class SessionsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public SessionsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SessionDto>>> GetSessions()
    {
        var sessions = await _context.Sessions.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<SessionDto>>(sessions));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SessionDto>> GetSession(int id)
    {
        var session = await _context.Sessions.FindAsync(id);
        if (session == null)
            return NotFound();

        return Ok(_mapper.Map<SessionDto>(session));
    }

    [HttpPost]
    public async Task<ActionResult<SessionDto>> CreateSession(CreateSessionDto createSessionDto)
    {
        var session = _mapper.Map<Session>(createSessionDto);
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSession), new { id = session.SessionId }, _mapper.Map<SessionDto>(session));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSession(int id, CreateSessionDto updateSessionDto)
    {
        var session = await _context.Sessions.FindAsync(id);
        if (session == null)
            return NotFound();

        _mapper.Map(updateSessionDto, session);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSession(int id)
    {
        var session = await _context.Sessions.FindAsync(id);
        if (session == null)
            return NotFound();

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
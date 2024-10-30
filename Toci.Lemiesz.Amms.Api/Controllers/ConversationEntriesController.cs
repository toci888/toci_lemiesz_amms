using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ConversationEntriesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ConversationEntriesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConversationEntryDto>>> GetConversationEntries()
    {
        var entries = await _context.ConversationEntries.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<ConversationEntryDto>>(entries));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConversationEntryDto>> GetConversationEntry(int id)
    {
        var entry = await _context.ConversationEntries.FindAsync(id);
        if (entry == null)
            return NotFound();

        return Ok(_mapper.Map<ConversationEntryDto>(entry));
    }

    [HttpPost]
    public async Task<ActionResult<ConversationEntryDto>> CreateConversationEntry(CreateConversationEntryDto createEntryDto)
    {
        var entry = _mapper.Map<ConversationEntry>(createEntryDto);
        _context.ConversationEntries.Add(entry);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetConversationEntry), new { id = entry.EntryId }, _mapper.Map<ConversationEntryDto>(entry));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateConversationEntry(int id, CreateConversationEntryDto updateEntryDto)
    {
        var entry = await _context.ConversationEntries.FindAsync(id);
        if (entry == null)
            return NotFound();

        _mapper.Map(updateEntryDto, entry);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConversationEntry(int id)
    {
        var entry = await _context.ConversationEntries.FindAsync(id);
        if (entry == null)
            return NotFound();

        _context.ConversationEntries.Remove(entry);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

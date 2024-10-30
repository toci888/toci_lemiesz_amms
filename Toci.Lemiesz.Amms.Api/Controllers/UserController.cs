using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UsersController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<User>(createUserDto);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, _mapper.Map<UserDto>(user));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, CreateUserDto updateUserDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        _mapper.Map(updateUserDto, user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

[ApiController]
[Route("api/[controller]")]
public class TherapeuticNotesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TherapeuticNotesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TherapeuticNoteDto>>> GetNotes()
    {
        var notes = await _context.TherapeuticNotes.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<TherapeuticNoteDto>>(notes));
    }

    [HttpPost]
    public async Task<ActionResult<TherapeuticNoteDto>> AddNote([FromBody] TherapeuticNoteDto noteDto)
    {
        var note = _mapper.Map<TherapeuticNote>(noteDto);
        _context.TherapeuticNotes.Add(note);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetNotes), new { id = note.Id }, _mapper.Map<TherapeuticNoteDto>(note));
    }
}

[ApiController]
[Route("api/[controller]")]
public class MedicationsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public MedicationsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedicationDto>>> GetMedications()
    {
        var medications = await _context.Medications.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<MedicationDto>>(medications));
    }

    [HttpPost]
    public async Task<ActionResult<MedicationDto>> AddMedication([FromBody] MedicationDto medicationDto)
    {
        var medication = _mapper.Map<Medication>(medicationDto);
        _context.Medications.Add(medication);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMedications), new { id = medication.Id }, _mapper.Map<MedicationDto>(medication));
    }
}

[ApiController]
[Route("api/[controller]")]
public class VisitsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public VisitsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VisitDto>>> GetVisits()
    {
        var visits = await _context.Visits.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<VisitDto>>(visits));
    }

    [HttpPost]
    public async Task<ActionResult<VisitDto>> AddVisit([FromBody] VisitDto visitDto)
    {
        var visit = _mapper.Map<Visit>(visitDto);
        _context.Visits.Add(visit);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetVisits), new { id = visit.Id }, _mapper.Map<VisitDto>(visit));
    }
}
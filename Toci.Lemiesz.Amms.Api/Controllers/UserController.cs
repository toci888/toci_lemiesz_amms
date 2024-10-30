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

// Controllers/TherapeuticNotesController.cs
[ApiController]
[Route("api/[controller]")]
public class TherapeuticNotesController : ControllerBase
{
    private static List<TherapeuticNote> notes = new List<TherapeuticNote>();
    private static int nextId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<TherapeuticNote>> GetNotes()
    {
        return Ok(notes);
    }

    [HttpPost]
    public ActionResult<TherapeuticNote> AddNote([FromBody] TherapeuticNoteDto noteDto)
    {
        var note = new TherapeuticNote { Id = nextId++, Text = noteDto.Text };
        notes.Add(note);
        return CreatedAtAction(nameof(GetNotes), new { id = note.Id }, note);
    }
}

// Controllers/MedicationsController.cs
[ApiController]
[Route("api/[controller]")]
public class MedicationsController : ControllerBase
{
    private static List<Medication> medications = new List<Medication>();
    private static int nextId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<Medication>> GetMedications()
    {
        return Ok(medications);
    }

    [HttpPost]
    public ActionResult<Medication> AddMedication([FromBody] MedicationDto medicationDto)
    {
        var medication = new Medication { Id = nextId++, Name = medicationDto.Name };
        medications.Add(medication);
        return CreatedAtAction(nameof(GetMedications), new { id = medication.Id }, medication);
    }
}

// Controllers/VisitsController.cs
[ApiController]
[Route("api/[controller]")]
public class VisitsController : ControllerBase
{
    private static List<Visit> visits = new List<Visit>();
    private static int nextId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<Visit>> GetVisits()
    {
        return Ok(visits);
    }

    [HttpPost]
    public ActionResult<Visit> AddVisit([FromBody] VisitDto visitDto)
    {
        var visit = new Visit { Id = nextId++, Date = visitDto.Date, Notes = visitDto.Notes };
        visits.Add(visit);
        return CreatedAtAction(nameof(GetVisits), new { id = visit.Id }, visit);
    }
}
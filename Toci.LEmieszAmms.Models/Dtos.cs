public class UserDto
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
}

public class CreateUserDto
{
    public string Name { get; set; }
    public string Role { get; set; }
}

public class SessionDto
{
    public int SessionId { get; set; }
    public int PatientId { get; set; }
    public int PsychiatristId { get; set; }
    public DateTime SessionStart { get; set; }
    public DateTime? SessionEnd { get; set; }
    public string ChatGPTSummary { get; set; }
    public string AudioTranscription { get; set; }
}

public class CreateSessionDto
{
    public int PatientId { get; set; }
    public int PsychiatristId { get; set; }
    public string ChatGPTSummary { get; set; }
    public string AudioTranscription { get; set; }
}

public class ConversationEntryDto
{
    public int EntryId { get; set; }
    public int SessionId { get; set; }
    public int SenderId { get; set; }
    public string MessageText { get; set; }
    public DateTime MessageTime { get; set; }
    public bool IsVoice { get; set; }
    public string ChatGPTResponse { get; set; }
}

public class CreateConversationEntryDto
{
    public int SessionId { get; set; }
    public int SenderId { get; set; }
    public string MessageText { get; set; }
    public bool IsVoice { get; set; }
    public string ChatGPTResponse { get; set; }
}

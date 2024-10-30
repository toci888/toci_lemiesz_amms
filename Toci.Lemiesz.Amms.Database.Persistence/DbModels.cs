using System;
using System.Collections.Generic;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Role { get; set; } // "patient" or "psychiatrist"
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Relacje
    public ICollection<Session> PatientSessions { get; set; }
    public ICollection<Session> PsychiatristSessions { get; set; }
    public ICollection<ConversationEntry> ConversationEntries { get; set; }
}

public class Session
{
    public int SessionId { get; set; }
    public int PatientId { get; set; }
    public int PsychiatristId { get; set; }
    public DateTime SessionStart { get; set; } = DateTime.UtcNow;
    public DateTime? SessionEnd { get; set; }
    public string ChatGPTSummary { get; set; }
    public string AudioTranscription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Relacje
    public User Patient { get; set; }
    public User Psychiatrist { get; set; }
    public ICollection<ConversationEntry> ConversationEntries { get; set; }
}

public class ConversationEntry
{
    public int EntryId { get; set; }
    public int SessionId { get; set; }
    public int SenderId { get; set; }
    public string MessageText { get; set; }
    public DateTime MessageTime { get; set; } = DateTime.UtcNow;
    public bool IsVoice { get; set; } = false;
    public string ChatGPTResponse { get; set; }

    // Relacje
    public Session Session { get; set; }
    public User Sender { get; set; }
}

public class SessionReport
{
    public int SessionId { get; set; }
    public string PatientName { get; set; }
    public string PsychiatristName { get; set; }
    public DateTime SessionStart { get; set; }
    public DateTime? SessionEnd { get; set; }
    public double? SessionDurationMinutes { get; set; }
    public int SummaryLength { get; set; }
    public int TotalMessages { get; set; }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<ConversationEntry> ConversationEntries { get; set; }
    public DbSet<SessionReport> SessionReports { get; set; }
    public DbSet<TherapeuticNote> TherapeuticNotes { get; set; }
    public DbSet<Medication> Medications { get; set; }
    public DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=amms;Username=postgres;Password=beatka");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Konfiguracja tabeli Users
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Role);

        // Konfiguracja tabeli Sessions
        modelBuilder.Entity<Session>()
            .HasKey(s => s.SessionId);

        modelBuilder.Entity<Session>()
            .HasOne(s => s.Patient)
            .WithMany(u => u.PatientSessions)
            .HasForeignKey(s => s.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Session>()
            .HasOne(s => s.Psychiatrist)
            .WithMany(u => u.PsychiatristSessions)
            .HasForeignKey(s => s.PsychiatristId)
            .OnDelete(DeleteBehavior.Restrict);

        // Konfiguracja tabeli ConversationEntries
        modelBuilder.Entity<ConversationEntry>()
            .HasKey(ce => ce.EntryId);

        modelBuilder.Entity<ConversationEntry>()
            .HasOne(ce => ce.Session)
            .WithMany(s => s.ConversationEntries)
            .HasForeignKey(ce => ce.SessionId);

        modelBuilder.Entity<ConversationEntry>()
            .HasOne(ce => ce.Sender)
            .WithMany(u => u.ConversationEntries)
            .HasForeignKey(ce => ce.SenderId);

        modelBuilder.Entity<ConversationEntry>()
            .Property(ce => ce.MessageText)
            .IsRequired();

        // Tworzenie indeksów
        modelBuilder.Entity<ConversationEntry>()
            .HasIndex(ce => ce.SessionId);

        modelBuilder.Entity<SessionReport>()
            .HasNoKey() // Widok nie ma klucza głównego
            .ToView("session_report"); // Nazwa widoku w bazie danych

        // Pozostałe konfiguracje tabel
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<Session>()
            .HasKey(s => s.SessionId);

        modelBuilder.Entity<ConversationEntry>()
            .HasKey(ce => ce.EntryId);
    }
}

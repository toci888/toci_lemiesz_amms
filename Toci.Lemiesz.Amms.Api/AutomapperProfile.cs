using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();

        CreateMap<Session, SessionDto>();
        CreateMap<CreateSessionDto, Session>();

        CreateMap<ConversationEntry, ConversationEntryDto>();
        CreateMap<CreateConversationEntryDto, ConversationEntry>();

        // Therapeutic Note mappings
        CreateMap<TherapeuticNote, TherapeuticNoteDto>();
        CreateMap<TherapeuticNoteDto, TherapeuticNote>();

        // Medication mappings
        CreateMap<Medication, MedicationDto>();
        CreateMap<MedicationDto, Medication>();

        // Visit mappings
        CreateMap<Visit, VisitDto>();
        CreateMap<VisitDto, Visit>();
    }
}
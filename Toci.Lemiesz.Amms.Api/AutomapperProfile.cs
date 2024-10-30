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
    }
}
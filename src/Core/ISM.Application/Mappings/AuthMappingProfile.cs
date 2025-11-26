using AutoMapper;
using ISM.Application.DTOs.Auth;
using ISM.Domain.Entities;

namespace ISM.Application.Mappings;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<ParticipantProfile, ParticipantRegistrationResultDto>()
            .ConstructUsing(profile => new ParticipantRegistrationResultDto(
                profile.ApplicationUserId ?? Guid.Empty,
                profile.Id,
                profile.ContactInfo.Email,
                profile.FullName,
                profile.ParticipantType.ToString()));
    }
}

using AutoMapper;
using ISM.Application.Features.Ideas.Dtos;
using ISM.Domain.Entities;

namespace ISM.Application.Common.Mappings;

public class IdeaMappingProfile : Profile
{
    public IdeaMappingProfile()
    {
        CreateMap<Idea, IdeaListItemDto>()
            .ForMember(d => d.IdeaCode, opt => opt.MapFrom(s => s.IdeaCode.Value));
        CreateMap<Idea, IdeaDetailDto>()
            .ForMember(d => d.IdeaCode, opt => opt.MapFrom(s => s.IdeaCode.Value));
        CreateMap<Idea, IdeaResultDto>()
            .ForMember(d => d.IdeaCode, opt => opt.MapFrom(s => s.IdeaCode.Value));
    }
}

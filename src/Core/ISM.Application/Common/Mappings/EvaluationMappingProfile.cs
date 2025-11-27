using AutoMapper;
using ISM.Application.Features.Evaluations.Dtos;
using ISM.Domain.Entities;

namespace ISM.Application.Common.Mappings;

public class EvaluationMappingProfile : Profile
{
    public EvaluationMappingProfile()
    {
        CreateMap<Idea, JudgeAssignedIdeaDto>()
            .ForMember(d => d.IdeaCode, opt => opt.MapFrom(s => s.IdeaCode.Value));
    }
}

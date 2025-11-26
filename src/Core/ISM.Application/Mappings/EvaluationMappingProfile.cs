using AutoMapper;
using ISM.Application.DTOs.Evaluations;
using ISM.Domain.Entities;

namespace ISM.Application.Mappings;

public class EvaluationMappingProfile : Profile
{
    public EvaluationMappingProfile()
    {
        CreateMap<Idea, JudgeAssignedIdeaDto>()
            .ForMember(d => d.IdeaCode, opt => opt.MapFrom(s => s.IdeaCode.Value));
    }
}

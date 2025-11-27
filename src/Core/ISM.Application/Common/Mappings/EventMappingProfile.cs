using AutoMapper;
using ISM.Application.Features.Events.Dtos;
using ISM.Domain.Entities;

namespace ISM.Application.Common.Mappings;

public class EventMappingProfile : Profile
{
    public EventMappingProfile()
    {
        CreateMap<CreateInnovationEventDto, InnovationEvent>();
        CreateMap<UpdateInnovationEventDto, InnovationEvent>();
        CreateMap<EvaluationCriteria, EvaluationCriteriaViewDto>();
        CreateMap<InnovationEvent, InnovationEventDetailDto>()
            .ForMember(d => d.Judges, opt => opt.MapFrom(s => s.EventJudges.Select(j => new EventJudgeDto(j.JudgeId, string.Empty))))
            .ForMember(d => d.Criteria, opt => opt.MapFrom(s => s.Criteria));
        CreateMap<InnovationEvent, InnovationEventListItemDto>();
    }
}

using AutoMapper;
using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Features.Evaluations.Dtos;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Features.Evaluations.Queries.GetIdeaForEvaluation;

internal class GetIdeaForEvaluationQueryHandler : IRequestHandler<GetIdeaForEvaluationQuery, IdeaEvaluationDetailDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetIdeaForEvaluationQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IdeaEvaluationDetailDto> Handle(GetIdeaForEvaluationQuery request, CancellationToken cancellationToken)
    {
        var idea = await _uow.Ideas.GetByIdAsync(request.IdeaId, cancellationToken) ?? throw new NotFoundException("Idea not found");
        if (!idea.Evaluations.Any(e => e.JudgeId == request.JudgeId))
            throw new ForbiddenException();

        var eventEntity = await _uow.InnovationEvents.GetWithDetailsAsync(idea.InnovationEventId, cancellationToken) ?? throw new NotFoundException("Event not found");
        var criteriaDtos = eventEntity.Criteria.Select(c => new IdeaEvaluationCriteriaScoreDto(c.Id, c.Title, c.Weight, c.MinScore, c.MaxScore)).ToList();

        return new IdeaEvaluationDetailDto
        {
            IdeaId = idea.Id,
            IdeaCode = idea.IdeaCode.Value,
            Title = idea.Title,
            Description = idea.Description,
            Criteria = criteriaDtos
        };
    }
}

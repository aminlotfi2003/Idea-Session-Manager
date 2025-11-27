using AutoMapper;
using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Features.Evaluations.Dtos;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Features.Evaluations.Queries.GetAssignedIdeasForJudge;

internal class GetAssignedIdeasForJudgeQueryHandler : IRequestHandler<GetAssignedIdeasForJudgeQuery, IReadOnlyCollection<JudgeAssignedIdeaDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetAssignedIdeasForJudgeQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<JudgeAssignedIdeaDto>> Handle(GetAssignedIdeasForJudgeQuery request, CancellationToken cancellationToken)
    {
        var eventEntity = await _uow.InnovationEvents.GetWithDetailsAsync(request.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");
        var ideas = eventEntity.Ideas.Where(i => i.Evaluations.Any(ev => ev.JudgeId == request.JudgeId)).ToList();
        return _mapper.Map<IReadOnlyCollection<JudgeAssignedIdeaDto>>(ideas);
    }
}

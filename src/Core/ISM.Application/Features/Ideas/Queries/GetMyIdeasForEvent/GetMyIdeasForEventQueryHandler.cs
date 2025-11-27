using AutoMapper;
using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Features.Ideas.Dtos;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Features.Ideas.Queries.GetMyIdeasForEvent;

internal class GetMyIdeasForEventQueryHandler : IRequestHandler<GetMyIdeasForEventQuery, IReadOnlyCollection<IdeaListItemDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetMyIdeasForEventQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<IdeaListItemDto>> Handle(GetMyIdeasForEventQuery request, CancellationToken cancellationToken)
    {
        var participant = await _uow.ParticipantProfiles.GetByUserIdAsync(request.CurrentUserId, cancellationToken) ?? throw new NotFoundException("Participant profile not found");
        var ideas = (await _uow.Ideas.GetByEventIdAsync(request.EventId, cancellationToken))
            .Where(i => i.ConfidentialLink.ParticipantProfileId == participant.Id)
            .ToList();
        return _mapper.Map<IReadOnlyCollection<IdeaListItemDto>>(ideas);
    }
}

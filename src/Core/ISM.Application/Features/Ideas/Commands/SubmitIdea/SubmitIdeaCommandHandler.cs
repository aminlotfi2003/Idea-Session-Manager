using AutoMapper;
using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Features.Ideas.Dtos;
using ISM.Domain.Entities;
using ISM.Domain.Enums;
using ISM.Domain.ValueObjects;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Features.Ideas.Commands.SubmitIdea;

internal class SubmitIdeaCommandHandler : IRequestHandler<SubmitIdeaCommand, IdeaDetailDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public SubmitIdeaCommandHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IdeaDetailDto> Handle(SubmitIdeaCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _uow.InnovationEvents.GetByIdAsync(request.Idea.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");
        if (eventEntity.Status != EventStatus.IdeaSubmissionOpen)
            throw new BusinessRuleViolationException("Idea submission is not open for this event.");

        if (DateTimeOffset.UtcNow > eventEntity.IdeaSubmissionEnd)
            throw new BusinessRuleViolationException("Idea submission window has closed.");

        var participant = await _uow.ParticipantProfiles.GetByUserIdAsync(request.CurrentUserId, cancellationToken);
        if (participant is null)
        {
            var participantType = eventEntity.AllowedParticipantGroup == AllowedParticipantGroup.Customer
                ? ParticipantType.Customer
                : ParticipantType.Staff;

            participant = ParticipantProfile.Create(
                request.Idea.ParticipantName,
                request.Idea.ParticipantDepartment,
                ParticipantContactInfo.Create(request.Idea.ParticipantEmail, string.Empty),
                participantType,
                DateTimeOffset.UtcNow,
                request.CurrentUserId);
            await _uow.ParticipantProfiles.AddAsync(participant, cancellationToken);
        }

        var ideaCode = IdeaCode.Create($"IDEA-{Guid.NewGuid():N}"[..8].ToUpperInvariant());
        var idea = Idea.Submit(
            request.Idea.EventId,
            ideaCode,
            request.Idea.Title,
            request.Idea.Description,
            request.Idea.Requirements,
            request.Idea.ProposedImplementation,
            request.Idea.ValueProposition,
            participant.Id,
            "encrypted"
        );

        await _uow.Ideas.AddAsync(idea, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return _mapper.Map<IdeaDetailDto>(idea);
    }
}

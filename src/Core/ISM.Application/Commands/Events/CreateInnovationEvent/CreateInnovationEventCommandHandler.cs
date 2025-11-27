using AutoMapper;
using ISM.Application.Abstractions.Repositories;
using ISM.Application.DTOs.Events;
using ISM.Domain.Entities;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Commands.Events.CreateInnovationEvent;

internal class CreateInnovationEventCommandHandler : IRequestHandler<CreateInnovationEventCommand, InnovationEventDetailDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CreateInnovationEventCommandHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<InnovationEventDetailDto> Handle(CreateInnovationEventCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Event;
        var overlap = await _uow.InnovationEvents.AnyOverlappingAsync(dto.IdeaSubmissionStart, dto.IdeaSubmissionEnd, cancellationToken);
        if (overlap)
            throw new ConflictException("An event already exists in the specified date range.");

        var entity = InnovationEvent.Create(
            dto.Title,
            dto.Description,
            dto.Goals,
            dto.AllowedParticipantGroup,
            dto.IdeaSubmissionStart,
            dto.IdeaSubmissionEnd,
            Guid.Empty,
            dto.RulesDocumentPath
        );

        await _uow.InnovationEvents.AddAsync(entity, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InnovationEventDetailDto>(entity);
    }
}

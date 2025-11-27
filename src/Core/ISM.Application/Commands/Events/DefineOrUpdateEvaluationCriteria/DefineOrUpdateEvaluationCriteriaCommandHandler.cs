using ISM.Application.Abstractions.Repositories;
using ISM.Domain.Entities;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Commands.Events.DefineOrUpdateEvaluationCriteria;

internal class DefineOrUpdateEvaluationCriteriaCommandHandler : IRequestHandler<DefineOrUpdateEvaluationCriteriaCommand>
{
    private readonly IUnitOfWork _uow;

    public DefineOrUpdateEvaluationCriteriaCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(DefineOrUpdateEvaluationCriteriaCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _uow.InnovationEvents.GetWithDetailsAsync(request.Payload.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");
        eventEntity.Criteria.Clear();

        foreach (var criterion in request.Payload.Criteria)
        {
            var entity = EvaluationCriteria.Create(
                request.Payload.EventId,
                criterion.Title,
                criterion.Description,
                criterion.MinScore,
                criterion.MaxScore,
                criterion.Weight,
                criterion.Order);
            eventEntity.AddEvaluationCriteria(entity);
        }

        await _uow.SaveChangesAsync(cancellationToken);
    }
}

using ISM.Application.Abstractions.Repositories;
using ISM.Domain.Entities;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Commands.Evaluations.SubmitIdeaEvaluation;

internal class SubmitIdeaEvaluationCommandHandler : IRequestHandler<SubmitIdeaEvaluationCommand>
{
    private readonly IUnitOfWork _uow;

    public SubmitIdeaEvaluationCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(SubmitIdeaEvaluationCommand request, CancellationToken cancellationToken)
    {
        var idea = await _uow.Ideas.GetWithDetailsAsync(request.Evaluation.IdeaId, cancellationToken) ?? throw new NotFoundException("Idea not found");
        var evaluation = idea.Evaluations.FirstOrDefault(e => e.JudgeId == request.JudgeId);
        if (evaluation is null)
        {
            evaluation = IdeaEvaluation.Create(request.Evaluation.IdeaId, request.JudgeId);
            idea.AddEvaluation(evaluation);
            await _uow.IdeaEvaluations.AddAsync(evaluation, cancellationToken);
        }

        double totalWeight = 0;
        double weightedScore = 0;
        foreach (var scoreDto in request.Evaluation.Scores)
        {
            var criteria = await _uow.EvaluationCriteria.GetByIdAsync(scoreDto.CriteriaId, cancellationToken) ?? throw new NotFoundException("Criteria not found");
            if (scoreDto.Score < criteria.MinScore || scoreDto.Score > criteria.MaxScore)
                throw new BusinessRuleViolationException("Score is out of range for criteria");

            var score = EvaluationScore.Create(evaluation.Id, criteria.Id, scoreDto.Score, scoreDto.Comment);
            evaluation.AddScore(score);
            totalWeight += criteria.Weight;
            weightedScore += scoreDto.Score * criteria.Weight;
        }

        var finalWeighted = totalWeight > 0 ? weightedScore / totalWeight : (double?)null;
        evaluation.SetDecision(request.Evaluation.Decision, request.Evaluation.Comments, finalWeighted);

        await _uow.SaveChangesAsync(cancellationToken);
    }
}

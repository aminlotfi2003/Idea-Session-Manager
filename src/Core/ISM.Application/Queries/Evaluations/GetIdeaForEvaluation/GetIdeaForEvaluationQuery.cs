using ISM.Application.DTOs.Evaluations;
using MediatR;

namespace ISM.Application.Queries.Evaluations.GetIdeaForEvaluation;

public record GetIdeaForEvaluationQuery(Guid IdeaId, Guid JudgeId) : IRequest<IdeaEvaluationDetailDto>;

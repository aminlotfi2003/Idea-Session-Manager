using ISM.Application.Features.Evaluations.Dtos;
using MediatR;

namespace ISM.Application.Features.Evaluations.Queries.GetIdeaForEvaluation;

public record GetIdeaForEvaluationQuery(Guid IdeaId, Guid JudgeId) : IRequest<IdeaEvaluationDetailDto>;

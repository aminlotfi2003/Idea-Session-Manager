using ISM.Application.Features.Evaluations.Dtos;
using MediatR;

namespace ISM.Application.Features.Evaluations.Commands.SubmitIdeaEvaluation;

public record SubmitIdeaEvaluationCommand(Guid JudgeId, SubmitIdeaEvaluationDto Evaluation) : IRequest;

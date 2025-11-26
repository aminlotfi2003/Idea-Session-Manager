using ISM.Application.DTOs.Evaluations;
using MediatR;

namespace ISM.Application.Commands.Evaluations.SubmitIdeaEvaluation;

public record SubmitIdeaEvaluationCommand(Guid JudgeId, SubmitIdeaEvaluationDto Evaluation) : IRequest;

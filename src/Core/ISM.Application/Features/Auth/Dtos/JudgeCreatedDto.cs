namespace ISM.Application.Features.Auth.Dtos;

public sealed record JudgeCreatedDto(Guid UserId, string Email, string TemporaryPassword);

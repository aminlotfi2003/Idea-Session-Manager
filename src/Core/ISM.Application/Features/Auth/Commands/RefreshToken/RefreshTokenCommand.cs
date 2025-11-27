using ISM.Application.Features.Auth.Dtos;
using MediatR;

namespace ISM.Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<RefreshTokenResponseDto>;

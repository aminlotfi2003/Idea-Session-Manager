using ISM.Application.DTOs.Auth;
using MediatR;

namespace ISM.Application.Commands.Auth.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<RefreshTokenResponseDto>;

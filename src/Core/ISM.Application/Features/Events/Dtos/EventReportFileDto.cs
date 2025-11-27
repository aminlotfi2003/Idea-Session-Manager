namespace ISM.Application.Features.Events.Dtos;

public record EventReportFileDto(string FileName, byte[] Content, string ContentType);

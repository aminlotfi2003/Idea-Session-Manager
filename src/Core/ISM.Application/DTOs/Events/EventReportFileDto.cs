namespace ISM.Application.DTOs.Events;

public record EventReportFileDto(string FileName, byte[] Content, string ContentType);

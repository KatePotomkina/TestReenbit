namespace UploadFile.Interfaces;

public interface IBlobStorage
{
    Task<string> UploadFilesAsync(string? filename, string? contecntType, Stream fileStream);
}
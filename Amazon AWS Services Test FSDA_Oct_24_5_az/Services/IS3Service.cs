namespace Amazon_AWS_Services_Test_FSDA_Oct_24_5_az.Services;

public interface IStorageService
{
    Task<string> UploadFileAsync(IFormFile file);
    Task DeleteFileAsync(string fileUrl);
}

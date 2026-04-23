using Amazon.S3;
using Amazon.S3.Model;

namespace MyApp.Services;

public class S3Service : IStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly string _region;

    public S3Service(IConfiguration configuration)
    { 
        var awsSection = configuration.GetSection("AWS");
        var accessKey = awsSection["AccessKey"];
        var secretKey = awsSection["SecretKey"];
        _region = awsSection["Region"]!;
        _bucketName = awsSection["BucketName"]!;

        var credentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);
        var config = new AmazonS3Config
        {
            RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(_region)
        };
        _s3Client = new AmazonS3Client(credentials,config);
    }

    public async Task DeleteFileAsync(string fileUrl)
    {
        var uri = new Uri(fileUrl);
        var key = uri.AbsolutePath.TrimStart('/');

        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = key
        };

        await _s3Client.DeleteObjectAsync(deleteRequest);
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName);
        var key = $"products/{Guid.NewGuid()}{fileExtension}";

        using var stream = file.OpenReadStream() ;

        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            InputStream = stream,
            ContentType = file.ContentType
        };

        await _s3Client.PutObjectAsync(putRequest) ;

        var url = $"https://{_bucketName}.s3.{_region}.amazonaws.com/{key}";
        return url ;
    }
}

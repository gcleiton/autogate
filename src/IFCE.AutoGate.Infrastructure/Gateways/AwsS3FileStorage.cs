using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using IFCE.AutoGate.Core.Settings;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace IFCE.AutoGate.Infrastructure.Gateways;

public class AwsS3FileStorage : IFileStorage
{
    private readonly string _bucket;
    private readonly AwsS3Settings _settings;

    public AwsS3FileStorage(IOptions<AwsS3Settings> settings, string bucket)
    {
        _settings = settings.Value;
        _bucket = bucket;
    }

    public async Task<string> Upload(IFormFile file)
    {
        var client = GetClient();

        var fileExtension = Path.GetExtension(file.FileName);
        var fileKey = Guid.NewGuid() + fileExtension;

        var fileBytes = new byte[file.Length];
        file.OpenReadStream().Read(fileBytes, 0, int.Parse(file.Length.ToString()));
        using var stream = new MemoryStream(fileBytes);

        var objectRequest = new PutObjectRequest
        {
            BucketName = _bucket,
            Key = fileKey,
            InputStream = stream,
            CannedACL = S3CannedACL.PublicRead
        };
        await client.PutObjectAsync(objectRequest);

        return fileKey;
    }

    private AmazonS3Client GetClient()
    {
        var credentials = new BasicAWSCredentials(_settings.AccessKey, _settings.Secret);
        return new AmazonS3Client(credentials, RegionEndpoint.USEast1);
    }
}

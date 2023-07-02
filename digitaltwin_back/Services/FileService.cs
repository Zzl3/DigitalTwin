using Aliyun.OSS;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace auth.Services
{
    public class FileService : IFileService
    {

        private readonly string accessKeyId;
        private readonly string accessKeySecret;
        private readonly string endpoint;
        private readonly string bucketName;
        static OssClient client;
        public FileService(IConfiguration configuration)
        {
            accessKeyId = configuration["OSS:accessKeyId"];
            accessKeySecret = configuration["OSS:accessKeySecret"];
            endpoint = configuration["OSS:endpoint"];
            bucketName = configuration["OSS:bucketName"];
            client = new OssClient(endpoint, accessKeyId, accessKeySecret);
        }
        public string UploadFile(IFormFile file, string fileName, string type = "file")
        {
            var key = $"{type}/{Guid.NewGuid().ToString().Replace("-", "")}/{fileName}";
            try
            {
                var fileStream = file.OpenReadStream();
                var res = client.PutObject(bucketName, key, fileStream);
            }
            catch (Exception e)
            {
                throw;
            }
            var fileUrl = "https://" + bucketName + '.' + endpoint + "/" + key;
            return fileUrl;
        }

        public string UploadFileStream(Stream stream, string fileName, string type = "file")
        {
            var key = $"{type}/{Guid.NewGuid().ToString().Replace("-", "")}/{fileName}";
            try
            {
                var res = client.PutObject(bucketName, key, stream);
            }
            catch (Exception e)
            {
                throw;
            }
            var fileUrl = "https://" + bucketName + '.' + endpoint + "/" + key;
            return fileUrl;
        }
    }
}

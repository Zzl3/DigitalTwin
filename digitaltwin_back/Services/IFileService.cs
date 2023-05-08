namespace auth.Services
{
    public interface IFileService
    {
        String UploadFile(IFormFile file, string fileName, string type = "file");

        String UploadFileStream(Stream stream, string fileName, string type = "file");
    }
}

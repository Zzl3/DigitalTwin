using auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace auth.Controllers
{
    [ApiController]
    [Route("/file")]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            this._fileService = fileService;
        }

        [HttpPost("upload")]
        public IActionResult UploadFilesByForm()
        {
            var code = 200;
            var msg = string.Empty;
            ICollection<object> data = new List<object>();
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                try
                {
                    Stream requestStream = file.OpenReadStream();
                    var fileURL = _fileService.UploadFileStream(requestStream, file.FileName);
                    
                    data.Add(new
                    {
                        fileName = file.FileName,
                        url = fileURL,
                        success = true
                    });
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.StackTrace);
                    msg = e.Message;
                }
            }
            return Ok(new
            {
                code,
                data,
                msg
            });
        }
    }
}

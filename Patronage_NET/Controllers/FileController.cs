using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Patronage_NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string FullPath;
        private readonly string FolderName;
        private readonly int FileSizeCap;
        private readonly int ContentCap;

        private readonly IWebHostEnvironment _env;

        public FileController(IWebHostEnvironment env)
        {
            _env = env;

            var DesktopPath = _env.ContentRootPath;
            FolderName = "TaskOne";
            FullPath = System.IO.Path.Combine(DesktopPath, FolderName);

            FileSizeCap = 255;
            ContentCap = 50;
        }

        private bool isContentValid(string content)
        {
            return content.Length <= ContentCap;
        }

        private void nextFileName(ref string path, string name, string content)
        {
            var fileLength = new System.IO.FileInfo(path).Length;
            var contentLength = content.Length;

            if ((fileLength + contentLength) > FileSizeCap)
            {
                int i = 1;
                while (true)
                {
                    path = System.IO.Path.Combine(FullPath, name + $"-{i}");

                    if (System.IO.File.Exists(path))
                    {
                        fileLength = new System.IO.FileInfo(path).Length;

                        if ((fileLength + contentLength) <= FileSizeCap)
                        {
                            break;
                        }
                    }
                    else if (!System.IO.File.Exists(path))
                    {
                        System.IO.File.Create(path);
                        break;
                    }

                    i++;
                }
            }
        }

        [HttpGet]
        public PhysicalFileResult GetAll(string name)
        {
            var filepath = System.IO.Path.Combine(FullPath, name);
            var contentType = "text/json";

            if (!System.IO.File.Exists(filepath))
            {
                throw new Exception(HttpStatusCode.NotFound.ToString());
            }

            return PhysicalFile(filepath, contentType, name, true); ;
        }

        [HttpPost]
        public async Task<string> Post([FromBody]Myfile myfile)
        {
            (var name, var content) = myfile.Deconstruct();

            if (!System.IO.Directory.Exists(FullPath))
            {
                System.IO.Directory.CreateDirectory(FullPath);
            }

            if (!isContentValid(content))
            {
                throw new Exception(HttpStatusCode.BadRequest.ToString());
            }

            var filepath = System.IO.Path.Combine(FullPath, name);

            await System.IO.File.WriteAllTextAsync(filepath, content);

            Response.StatusCode = (int)HttpStatusCode.OK;
            return $"Created {filepath}";
        }

        [HttpPut]
        public async Task<string> Put([FromBody]Myfile myfile)
        {
            (var name, var content) = myfile.Deconstruct();

            var filepath = System.IO.Path.Combine(FullPath, name);

            if (!System.IO.File.Exists(filepath))
            {
                throw new Exception(HttpStatusCode.BadRequest.ToString());
            }

            if (!isContentValid(content))
            {
                throw new Exception(HttpStatusCode.BadRequest.ToString());
            }

            nextFileName(ref filepath, name, content);

            await System.IO.File.AppendAllTextAsync(filepath, Environment.NewLine + content);

            Response.StatusCode = (int)HttpStatusCode.OK;
            return $"Updated {filepath}";
        }
    }
}

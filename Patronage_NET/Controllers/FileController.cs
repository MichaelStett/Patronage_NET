using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
        private readonly IConfiguration _config;

        public FileController(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;

            FolderName = _config.GetValue<string>("FileController:FolderName");
            FileSizeCap = _config.GetValue<int>("FileController:FileSizeCap");
            ContentCap = _config.GetValue<int>("FileController:ContentCap");

            FullPath = System.IO.Path.Combine(_env.ContentRootPath, FolderName);
        }

        private bool IsContentValid(string content)
        {
            return content.Length <= ContentCap;
        }

        private void NextFileName(ref string path, string name, string content)
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

        /// <summary>
        /// Get file sepcified by name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns> Requested file</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(200, Type = typeof(PhysicalFileResult))]
        [ProducesResponseType(404, Type = null)]
        [ProducesResponseType(500)]
        [HttpGet]
        public PhysicalFileResult Get(string fileName)
        {
            var filepath = System.IO.Path.Combine(FullPath, fileName);
            var contentType = MediaTypeNames.Text.Plain;

            if (!System.IO.File.Exists(filepath))
            {
                throw new Exception(HttpStatusCode.NotFound.ToString());
            }

            return PhysicalFile(filepath, contentType, fileName, true); ;
        }

        /// <summary>
        /// Create new file with specified name and content
        /// </summary>
        /// <param name="myfile"></param>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = null)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Myfile myfile)
        {
            var name = myfile.Name;
            var content = myfile.Content;

            if (!System.IO.Directory.Exists(FullPath))
            {
                System.IO.Directory.CreateDirectory(FullPath);
            }

            if (!IsContentValid(content))
            {
                throw new Exception(HttpStatusCode.BadRequest.ToString());
            }

            var filepath = System.IO.Path.Combine(FullPath, name);

            await System.IO.File.WriteAllTextAsync(filepath, content);

            return NoContent();
        }

        /// <summary>
        /// Update file with specified name with content
        /// </summary>
        /// <param name="myfile"></param>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = null)]
        [ProducesResponseType(404, Type = null)]
        [ProducesResponseType(500)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Myfile myfile)
        {
            var name = myfile.Name;
            var content = myfile.Content;

            var filepath = System.IO.Path.Combine(FullPath, name);

            if (!System.IO.File.Exists(filepath))
            {
                throw new Exception(HttpStatusCode.BadRequest.ToString());
            }

            if (!IsContentValid(content))
            {
                throw new Exception(HttpStatusCode.BadRequest.ToString());
            }

            NextFileName(ref filepath, name, content);

            await System.IO.File.AppendAllTextAsync(filepath, Environment.NewLine + content);

            return NoContent();
        }
    }
}

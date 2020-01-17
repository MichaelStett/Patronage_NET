using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Patronage_NET.Web.Controllers.Help
{
    public class Helper : IHelper
    {
        private readonly string FullPath;
        private readonly string FolderName;
        private readonly int FileSizeCap;
        private readonly int ContentCap;

        public readonly IWebHostEnvironment _env;
        public readonly IConfiguration _config;

        public Helper(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;

            ContentCap = _config.GetValue<int>("FileController:ContentCap");
            FileSizeCap = _config.GetValue<int>("FileController:FileSizeCap");
            FolderName = _config.GetValue<string>("FileController:DirectoryName");

            FullPath = System.IO.Path.Combine(_env.ContentRootPath, FolderName);
        }

        public bool IsContentValid(string content) => (content.Length <= ContentCap);

        public string GetNewPath(string path, string name, string content)
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
            return path;
        }

        public int GetContentCap() => ContentCap;
        public int GetFileSizeCap() => FileSizeCap;
        public string GetPath() => FullPath;
       
    }
}

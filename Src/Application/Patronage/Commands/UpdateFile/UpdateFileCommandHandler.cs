using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FileNotFoundException = Northwind.Domain.Exceptions.FileNotFoundException;

namespace Northwind.Application.Patronage.Commands.UpdateFile
{
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, bool>
    {
        public readonly IConfiguration _config;

        public UpdateFileCommandHandler(IConfiguration config)
        {
            _config = config;
        }

        private string GetNewPath(string path, string name, string content)
        {
            var SizeCap = _config.GetValue<int>("FilesOptions:SizeCap");
            var dirname = _config.GetValue<string>("FilesOptions:DirectoryName");

            var basepath = Environment.CurrentDirectory.ToString();


            var dirpath = Path.Combine(basepath, dirname);

            var fileLength = new FileInfo(path).Length;
            var contentLength = content.Length;

            if ((fileLength + contentLength) > SizeCap)
            {
                int i = 1;
                while (true)
                {
                    var filename = $"{name}-{i}";

                    path = Path.Combine(dirpath, filename);

                    if (File.Exists(path))
                    {
                        fileLength = new FileInfo(path).Length;

                        if ((fileLength + contentLength) <= SizeCap)
                        {
                            break;
                        }
                    }
                    else if (!File.Exists(path))
                    {
                        File.Create(path);
                        break;
                    }

                    i++;
                }
            }
            return path;
        }

        public async Task<bool> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            var filename = request.Name;
            var content = request.Content;

            var basepath = Environment.CurrentDirectory.ToString();

            var dirname = _config.GetValue<string>("FilesOptions:DirectoryName");

            var path = Path.Combine(basepath, dirname);

            var filepath = Path.Combine(path, filename);

            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException(filename);
            }

            var newpath = GetNewPath(filepath, filename, content);

            await File.AppendAllTextAsync(newpath,  $"{Environment.NewLine}{content}");

            return true;
        }
    }
}

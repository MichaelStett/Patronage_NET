using Microsoft.Extensions.Configuration;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System;

namespace Northwind.Application.Patronage.Commands.CreateFile
{
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, bool>
    {
        public readonly IConfiguration _config;

        public CreateFileCommandHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var filename = request.Name;
            var content = request.Content;

            var basepath = Environment.CurrentDirectory.ToString();

            var dirname = _config.GetValue<string>("FilesOptions:DirectoryName");

            var path = Path.Combine(basepath, dirname);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var filepath = Path.Combine(path, filename);

            await File.WriteAllTextAsync(filepath, content);

            return true;
        }
    }
}

using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Northwind.Application.Common.Models
{
    public class LogFile
    {
        public readonly IConfiguration _config;

        public LogFile(IConfiguration config)
        {
            _config = config;
        }

        public string FilePath()
        {
            var file = String.Concat(
                _config.GetValue<string>("LoggingOptions:FileName"),
                _config.GetValue<string>("LoggingOptions:FileExtension"));

            var dir = Path.Combine(
                Environment.CurrentDirectory.ToString(),
                _config.GetValue<string>("LoggingOptions:DirectoryName"));

            return Path.Combine(dir, file);
        }

        public async Task<string []> Get()
        {
            var path = FilePath();

            return await File.ReadAllLinesAsync(path);
        }

        public async Task Append(string line)
        {
            var path = FilePath();

            await File.AppendAllTextAsync(path, line);

            return;
        }
    }
}

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Northwind.Application.Patronage.Queries.GetReversedString
{
    public class GetReversedStringQueryHandler : IRequestHandler<GetReversedStringQuery, string>
    {
        public readonly IConfiguration _config;

        public GetReversedStringQueryHandler(IConfiguration config)
        {
            _config = config;
        }

        private string Reverse(string text)
        {
            var arr = text.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        private string LogMessage(string from, string to)
        {
            var dateTime = DateTime.UtcNow.ToString("dd-MM-yy HH:mm:ss");
            return $"[{dateTime}]: {from} -> {to} {Environment.NewLine}";
        }

        public async Task<string> Handle(GetReversedStringQuery request, CancellationToken cancellationToken)
        {
            var text = request.Text;
            var reversed = Reverse(text);

            var log = LogMessage(text, reversed); 

            var basepath = Environment.CurrentDirectory.ToString();

            var dirname = _config.GetValue<string>("LoggingOptions:DirectoryName");
            var filename = _config.GetValue<string>("LoggingOptions:FileName");

            var path = Path.Combine(basepath, dirname);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var filepath = Path.Combine(path, filename);

            await File.AppendAllTextAsync(filepath, log);

            return reversed;
        }
    }
}

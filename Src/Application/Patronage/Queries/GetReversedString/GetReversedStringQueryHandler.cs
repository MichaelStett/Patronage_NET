using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Northwind.Application.Common.Models;
using Northwind.Common;

namespace Northwind.Application.Patronage.Queries.GetReversedString
{
    public class GetReversedStringQueryHandler : IRequestHandler<GetReversedStringQuery, string>
    {
        private readonly IConfiguration _config;
        private readonly IDateTime _dateTime;

        public GetReversedStringQueryHandler(IConfiguration config, IDateTime dateTime)
        {
            _config = config;
            _dateTime = dateTime;
        }

        public async Task<string> Handle(GetReversedStringQuery request, CancellationToken cancellationToken)
        {
            var text = request.Text;

            var reversed = new String(text.Reverse().ToArray());

            var logfile = new LogFile(_config);

            var message = $"[{_dateTime.Now.ToString("dd-MM-yy HH:mm:ss")}]: {text} -> {reversed} {Environment.NewLine}";

            await logfile.Append(message);

            return reversed;
        }
    }
}

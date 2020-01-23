using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Northwind.Application.Common.Models;

namespace Northwind.Application.Patronage.Queries.GetLogs
{
    public class GetLogsQueryHandler : IRequestHandler<GetLogsQuery, string[]>
    {
        public readonly IConfiguration _config;

        public GetLogsQueryHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string[]> Handle(GetLogsQuery request, CancellationToken cancellationToken)
        {
            var logfile = new LogFile(_config);

            return await logfile.Get();
        }
    }
}

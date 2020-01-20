using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace Northwind.Application.Patronage.Queries.GetLogs
{
    public class GetLogsQueryHandler : IRequestHandler<GetLogsQuery, string[]>
    {
        public GetLogsQueryHandler()
        {

        }

        public async Task<string[]> Handle(GetLogsQuery request, CancellationToken cancellationToken)
        {
            var filepath = @"C:\Users\Michal\Desktop\Directory\logs.txt";

            var lines = await File.ReadAllLinesAsync(filepath);

            return lines;
        }
    }
}

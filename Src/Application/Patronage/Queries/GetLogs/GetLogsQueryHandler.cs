using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Northwind.Application.Common.Models;
using Northwind.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Northwind.Application.Patronage.Queries.GetLogs
{
    public class GetLogsQueryHandler : IRequestHandler<GetLogsQuery, string[]>
    {
        public readonly IConfiguration _config;
        private readonly INorthwindDbContext _context;

        public GetLogsQueryHandler(IConfiguration config, INorthwindDbContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<string[]> Handle(GetLogsQuery request, CancellationToken cancellationToken)
        {
            var file = await _context.MyLogs
                             .FirstAsync();

            var data = file.Content.ToArray();

            return data;
        }
    }
}

using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Northwind.Application.Common.Models;
using Northwind.Common;
using Northwind.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Application.Patronage.Queries.GetReversedString
{
    public class GetReversedStringQueryHandler : IRequestHandler<GetReversedStringQuery, string>
    {
        private readonly IConfiguration _config;
        private readonly IDateTime _dateTime;
        private readonly INorthwindDbContext _context;

        public GetReversedStringQueryHandler(IConfiguration config, IDateTime dateTime, INorthwindDbContext context)
        {
            _config = config;
            _dateTime = dateTime;
            _context = context;
        }

        public async Task<string> Handle(GetReversedStringQuery request, CancellationToken cancellationToken)
        {
            var text = request.Text;

            var reversed = new String(text.Reverse().ToArray());

            var message = $"[{_dateTime.Now.ToString("dd-MM-yy HH:mm:ss")}]: {text} -> {reversed} ";

            var name = _config.GetValue<string>("LoggingOptions:FileName");

            var log = await _context.MyLogs.FirstAsync(l => l.LogName == name);

            log.Content.ToList().Add(message);

            _context.MyLogs.Update(log);

            await _context.SaveChangesAsync(cancellationToken);

            return reversed;
        }
    }
}

using MediatR;
using Northwind.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Application.Patronage.Queries.GetFile
{
    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, byte[]>
    {
        private readonly INorthwindDbContext _context;

        public GetFileQueryHandler(INorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {

            var file = await _context.MyFiles
                             .FirstAsync(m => m.FileName == request.Name);

            var data = file.Data;

            return data;
        }
    }
}

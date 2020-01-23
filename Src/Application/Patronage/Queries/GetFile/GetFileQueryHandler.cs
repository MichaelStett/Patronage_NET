using MediatR;
using Microsoft.Extensions.Configuration;
using Northwind.Application.Common.Interfaces;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Application.Patronage.Queries.GetFile
{
    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, byte[]>
    {
        private readonly IConfiguration _config;
        private readonly INorthwindDbContext _context;

        public GetFileQueryHandler(IConfiguration config, INorthwindDbContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<byte[]> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {

            var file = await _context.MyFiles.FirstOrDefaultAsync(m => m.FileName == request.Name);

            var data = file.Data.ToArray();

            return data;
        }
    }
}

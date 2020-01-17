using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Patronage_NET.Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System;
using System.Net;

namespace Patronage_NET.Application.Files.Queries.GetFile
{
    public class GetFileQuery : IRequest<FileVm>
    {
        public string FileName { get; set; }

        public class GetFileQueryHandler : IRequestHandler<GetFileQuery, FileVm>
        {
            private readonly IPatronageDbContext _context;
            private readonly IMapper _mapper;
            
            public GetFileQueryHandler(IPatronageDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FileVm> Handle(GetFileQuery request, CancellationToken cancellationToken)
            {
                var vm = await _context.Files
                    .Where(f => f.Name == request.FileName)
                    .ProjectTo<FileVm>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);

                return vm;
            }
        }
    }
}

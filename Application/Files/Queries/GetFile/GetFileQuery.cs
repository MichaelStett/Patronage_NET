using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Patronage_NET.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System;
using System.Net;

namespace Patronage_NET.Application.Files.Queries.GetFile
{
    public class GetFileQuery : ControllerBase, IRequest<FileStreamResult>
    {
        public string FileName { get; set; }

        public class GetFileQueryHandler : IRequestHandler<GetFileQuery, FileStreamResult>
        {
            private readonly IPatronageDbContext _context;
            private readonly IMapper _mapper;
            
            public GetFileQueryHandler(IPatronageDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FileStreamResult> Handle(GetFileQuery request, CancellationToken cancellationToken)
            {

                var myfile = _context.Files
                    .Where(f => f.Name == request.FileName).Single();

                var filepath = myfile.FilePath;
                var filename = myfile.Name;

                var contentType = MediaTypeNames.Text.Plain;

                if (!System.IO.File.Exists(filepath))
                {
                    throw new Exception(HttpStatusCode.NotFound.ToString());
                }

                var stream = System.IO.File.OpenRead(filepath);

                return new FileStreamResult(stream, contentType);
            }
        }
    }
}

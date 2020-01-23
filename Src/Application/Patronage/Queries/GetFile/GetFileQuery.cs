using MediatR;
using System;
using System.Collections.Generic;

namespace Northwind.Application.Patronage.Queries.GetFile
{
    public class GetFileQuery : IRequest<byte[]>
    {
        public string Name { get; set; }
    }
}

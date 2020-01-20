using MediatR;

namespace Northwind.Application.Patronage.Queries.GetFile
{
    public class GetFileQuery : IRequest<string>
    {
        public string Name { get; set; }
    }
}

using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Patronage.Queries.GetFile
{
    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, string>
    {
        public GetFileQueryHandler()
        {

        }

        public async Task<string> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var name = request.Name;

            return name + " GET ";
        }
    }
}

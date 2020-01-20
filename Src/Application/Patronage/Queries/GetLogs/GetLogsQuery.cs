using MediatR;

namespace Northwind.Application.Patronage.Queries.GetLogs
{
    public class GetLogsQuery : IRequest<string[]>
    {
    }
}

using MediatR;

namespace Northwind.Application.Patronage.Queries.GetReversedString
{
    public class GetReversedStringQuery : IRequest<string>
    {
        public string Text { get; set; }
    }
}

using MediatR;
using Northwind.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain.Entities;
using System.Text;

namespace Northwind.Application.Patronage.Commands.CreateFile
{
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, bool>
    {
        private readonly INorthwindDbContext _context;

        public CreateFileCommandHandler(INorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var name = request.Name;
            var data = Encoding.ASCII.GetBytes(request.Data);

            var file = await _context.MyFiles
                             .FirstAsync(m => m.FileName == name);

            if (file != null)
                _context.MyFiles.Remove(file);

            var entity = new MyFile
            {
                FileName = name,
                Data = data
            };

            _context.MyFiles.Add(entity);

            return true;
        }
    }
}

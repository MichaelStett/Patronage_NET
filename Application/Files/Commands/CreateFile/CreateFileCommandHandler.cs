using MediatR;
using Patronage_NET.Application.Common.Interfaces;
using Patronage_NET.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Patronage_NET.Application.Files.Commands.CreateFile
{
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, int>
    {
        private readonly IPatronageDbContext _context;
        private const string path = @"C:\Users\Michal\Desktop\Dirs";
        public CreateFileCommandHandler(IPatronageDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var name = request.Name;
            var content = request.Content;

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            //if (!_helper.IsContentValid(content))
            //{
            //    throw new Exception(HttpStatusCode.BadRequest.ToString());
            //}

            var filepath = System.IO.Path.Combine(path, name);

            await System.IO.File.WriteAllTextAsync(filepath, content);

            var entity = new Myfile
            {
                Name = name,
                FilePath = filepath
            };

            await _context.Files.AddAsync(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

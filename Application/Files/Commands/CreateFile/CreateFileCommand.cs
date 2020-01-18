using MediatR;

namespace Patronage_NET.Application.Files.Commands.CreateFile
{
    public class CreateFileCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}

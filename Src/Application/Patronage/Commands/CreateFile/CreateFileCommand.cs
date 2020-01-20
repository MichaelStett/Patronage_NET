using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Application.Patronage.Commands.CreateFile
{
    public class CreateFileCommand : IRequest<bool>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
    }
}

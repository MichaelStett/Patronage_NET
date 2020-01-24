using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Application.Patronage.Commands.UpdateFile
{
    public class UpdateFileCommand : IRequest<bool>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Data { get; set; }
    }
}

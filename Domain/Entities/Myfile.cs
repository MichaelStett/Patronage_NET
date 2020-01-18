using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Patronage_NET.Domain.Entities
{
    public class Myfile
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Content { get; set; }

        public string FilePath { get; set; }
    }
}

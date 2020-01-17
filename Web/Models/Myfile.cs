using System.ComponentModel.DataAnnotations;

namespace Patronage_NET.Web
{
    public class Myfile
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Content { get; set; }

    }
}

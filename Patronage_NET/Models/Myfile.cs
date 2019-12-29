using System.ComponentModel.DataAnnotations;

namespace Patronage_NET
{
    public class Myfile
    {
        public (string name, string content) Deconstruct() => (this.name, this.content);

        [Required]
        public string name { get; set; }

        [Required]
        [MaxLength(50)]
        public string content { get; set; }

    }
}

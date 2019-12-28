namespace Patronage_NET
{
    public class Myfile
    {
        public (string name, string content) Deconstruct() => (this.name, this.content);

        public string name { get; set; }
        public string content { get; set; }

    }
}

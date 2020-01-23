using Northwind.Domain.Common;
using System.Collections.Generic;

namespace Northwind.Domain.Entities
{
    public class MyFile : AuditableEntity
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public IEnumerable<string> Data { get; set; }
    }
}

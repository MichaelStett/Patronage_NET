using Northwind.Domain.Common;
using System.Collections.Generic;

namespace Northwind.Domain.Entities
{
    public class MyFile
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }   
}

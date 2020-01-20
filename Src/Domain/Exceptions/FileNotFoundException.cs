using System;

namespace Northwind.Domain.Exceptions
{
    public class FileNotFoundException : Exception
    {
        public FileNotFoundException(string name)
           : base($"File: '{name}' doesn't exist.")
        {
        }
    }
}

using Northwind.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Domain.Entities
{
    public class MyLog
    {
        public int LogId { get; set; }
        public string LogName { get; set; }
        public string[] Content { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Patronage_NET.Application.Files.Queries.GetFile
{
    public class FileVm
    {
        public string FileName { get; set; }

        public PhysicalFileResult Result { get; private set; }
    }
}

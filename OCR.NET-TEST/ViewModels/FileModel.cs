using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCR.NET_TEST.ViewModels
{
    public class FileModel
    {
        public List<IFormFile> Files{ get; set; }
    }
}

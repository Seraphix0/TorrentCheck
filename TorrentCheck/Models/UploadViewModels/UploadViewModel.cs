using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TorrentCheck.Models.UploadViewModels
{
    public class UploadViewModel
    {
        public IFormFile TorrentToDecode { get; set; }
        public string Description { get; set; }
    }
}

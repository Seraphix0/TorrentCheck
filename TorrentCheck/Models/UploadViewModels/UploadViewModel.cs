using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TorrentCheck.Models.UploadViewModels
{
    public class UploadViewModel
    {
        public IFormFile TorrentToDecode { get; set; }

        public string Description { get; set; }

        public IEnumerable<Category> Category { get; set; }

        public SelectList Categories { get; set; }

        public Category SelectedCategory { get; set; }
    }
}

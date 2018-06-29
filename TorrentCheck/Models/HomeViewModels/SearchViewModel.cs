using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static TorrentCheck.Models.Torrent;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TorrentCheck.Models.HomeViewModels
{
    public class SearchViewModel
    {
        [Display(Name = "Title")]
        public string Title { get; set; }

        public Category Category { get; set; }

        public IEnumerable<SelectListItem> RemoteSources { get; set; }

        public string RemoteSource { get; set; }

        public bool IncludeUntrustedResults { get; set; }

        public bool ExcludeRemoteSources { get; set; }

        public List<Result> HTTPResults { get; set; }

        public List<Result> SQLResults { get; set; }
    }
}

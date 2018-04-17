using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TorrentCheck.Models.HomeViewModels
{
    public class SearchViewModel
    {
        [Key]
        [Required]
        [Display(Name = "Search")]
        public string Search { get; set; }

    }
}

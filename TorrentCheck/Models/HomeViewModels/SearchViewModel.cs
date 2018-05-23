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
        [Display(Name = "Title")]
        public string Title { get; set; }

        public List<Result> HTTPResults { get; set; }
        public List<Result> SQLResults { get; set; }
    }
}

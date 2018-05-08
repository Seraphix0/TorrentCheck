using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrentCheck.Models.HomeViewModels;

namespace TorrentCheck.Models
{
    public class Result
    {
        private string title;
        private bool trusted;
        private Category category;

        public string Title => title;
        public bool Trusted => trusted;

        public Result()
        {

        }

        public Result(string Title, bool Trusted)
        {
            title = Title;
            trusted = Trusted;
        }

        private enum Category
        {
            Application,
            Game,
            Movie,
            Audio
        }
    }
}

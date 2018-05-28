using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.Models
{
    public class TorrentRating
    {
        public TorrentRating(int id, string ratedBy, int value, string description)
        {
            Torrent_Id = id;
            UserName = ratedBy;
            Value = value;
            Description = description;
        }

        // Composite primary key Torrent_Id + UserName
        [ForeignKey ("Torrent")]
        public int Torrent_Id { get; set; }

        // [ForeignKey ("ApplicationUser")]
        public string UserName { get; set; } // Rated by user

        public int Value { get; set; }

        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.Models
{
    public class UserDownloads
    {
        public UserDownloads(int id, string ratedBy, int value, string description)
        {
            Torrent_Id = id;
            UserName = ratedBy;
            Value = value;
            Description = description;
        }

        // Composite primary key Torrent_Id + UserName
        [ForeignKey ("Torrent")]
        public int Torrent_Id { get; set; }

        public string UserName { get; set; } // Rated By

        public int Value { get; set; }

        public string Description { get; set; }
    }
}

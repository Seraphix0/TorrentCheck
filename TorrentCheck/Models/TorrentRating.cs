using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.Models
{
    public class TorrentRating
    {
        public TorrentRating(int id, int ratedBy, int value, string description)
        {
            Torrent_Id = id;
            User_Id = ratedBy;
            Value = value;
            Description = description;
        }

        public int Torrent_Id { get; set; }
        public int User_Id { get; set; } // Rated by user
        public int Value { get; set; }
        public string Description { get; set; }
    }
}

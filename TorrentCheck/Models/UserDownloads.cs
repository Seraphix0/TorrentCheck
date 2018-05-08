using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.Models
{
    public class UserDownloads
    {
        public UserDownloads(int id, int ratedBy, int value, string description)
        {
            Id = id;
            RatedBy = ratedBy;
            Value = value;
            Description = description;
        }

        public int Id { get; set; }
        public int RatedBy { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
    }
}

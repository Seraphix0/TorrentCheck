using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.Models
{
    public class UserRating
    {
        public UserRating(int id, int ratedBy, int value, string description)
        {
            User_Id = id;
            RatedBy_User_Id = ratedBy;
            Value = value;
            Description = description;
        }

        public int User_Id { get; set; } // User who is being rated
        public int RatedBy_User_Id { get; set; } // User who rated
        public int Value { get; set; }
        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.Models
{
    public class UserRating
    {
        public UserRating(int id, int ratedBy, int value, string description)
        {
            UserName = id;
            RatedBy_UserName = ratedBy;
            Value = value;
            Description = description;
        }

        // Composite primary key UserName + RatedBy_UserName
        [Required]
        // [ForeignKey ("ApplicationUser")]
        public int UserName { get; set; } // User who is being rated
        
        [Required]
        public int RatedBy_UserName { get; set; } // User who rated

        public int Value { get; set; }

        public string Description { get; set; }
    }
}

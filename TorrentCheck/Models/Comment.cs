using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.Models
{
    public class Comment
    {
        public Comment() { }

        /// <summary>
        /// Constructor for database inserts, with Id = 0 for auto-generated identity increments.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="torrent"></param>
        /// <param name="userName"></param>
        public Comment(string text, Torrent torrent, string userName)
        {
            Id = 0;
            Text = text;
            Torrent = torrent;
            UserName = userName;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Text { get; set; }

        public Torrent Torrent { get; set; }

        // [ForeignKey ("ApplicationUser")]
        public string UserName { get; set; }
    }
}

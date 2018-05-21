using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.Models
{
    public class Comment
    {
        public Comment(int id, string text, int torrent_Id, int commenter)
        {
            Id = id;
            Text = text;
            Torrent_Id = torrent_Id;
            User_Id = commenter;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public int Torrent_Id { get; set; }
        public int User_Id { get; set; }
    }
}

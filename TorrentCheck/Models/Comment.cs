using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.Models
{
    public class Comment
    {
        public Comment(int id, string text, int commenter)
        {
            Id = id;
            Text = text;
            Commenter = commenter;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public int Commenter { get; set; }
    }
}

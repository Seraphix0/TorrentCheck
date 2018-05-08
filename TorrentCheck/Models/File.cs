using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.Models
{
    public class File
    {
        public File(int id, string name, int size)
        {
            Id = id;
            Name = name;
            Size = size;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.Models
{
    public class File
    {
        public File() { }

        /// <summary>
        /// Constructor for database inserts, with Id = 0 for auto-generated identity increments.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="md5Sum"></param>
        /// <param name="fullPath"></param>
        /// <param name="torrent"></param>
        public File(string name, long size, string md5Sum, string fullPath, Torrent torrent)
        {
            Id = 0;
            Name = name;
            Size = size;
            Md5Sum = md5Sum;
            FullPath = fullPath;
            Torrent = torrent;
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public long Size { get; set; }

        //
        // Summary:
        //     [optional] 32-character hexadecimal string corresponding to the MD5 sum of the
        //     file. Rarely used.
        public string Md5Sum { get; set; }

        //
        // Summary:
        //     The full path of the file including file name.
        public string FullPath { get; set; }

        public Torrent Torrent { get; set; }
    }
}

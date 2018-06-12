using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorrentCheck.Models
{
    public class Torrent
    {
        public Torrent() { }

        public Torrent(int id, string title, long size, DateTime uploadDate, string infoHash, string userName, Category category, string magnetLink)
        {
            Id = id;
            Title = title;
            Size = size;
            UploadDate = uploadDate;
            InfoHash = infoHash;
            UserName = userName;
            Category = category;
            MagnetLink = magnetLink;
        }

        /// <summary>
        /// Constructor for database inserts, with Id = 0 for auto-generated identity increments.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="size"></param>
        /// <param name="uploadDate"></param>
        /// <param name="md5Sum"></param>
        /// <param name="user_Id"></param>
        /// <param name="category"></param>
        public Torrent(string title, long size, DateTime uploadDate, string infoHash, string filePath, string userName, Category category, string magnetLink)
        {
            Id = 0;
            Title = title;
            Size = size;
            UploadDate = uploadDate;
            InfoHash = infoHash;
            FilePath = filePath;
            UserName = userName;
            Category = category;
            MagnetLink = magnetLink;
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public long Size { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        public string InfoHash { get; set; }

        [Required]
        public string FilePath { get; set; }

        // [ForeignKey ("ApplicationUser")]
        [Required]
        public string UserName { get; set; } // Uploader

        public string MagnetLink { get; set; }

        public ICollection<File> Files { get; set; }

        public ICollection<Comment> Comments { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}

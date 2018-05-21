using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Services;

namespace TorrentCheck.Models
{
    public class Torrent
    {
        public Torrent(int id, string title, int size, DateTime uploadDate, string md5Sum, List<File> files, List<Comment> comments, int uploader)
        {
            Id = id;
            Title = title;
            Size = size;
            UploadDate = uploadDate;
            Md5Sum = md5Sum;
            Files = files;
            Comments = comments;
            User_Id = uploader;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int Size { get; set; }
        public DateTime UploadDate { get; set; }
        public string Md5Sum { get; set; }
        public List<File> Files { get; set; }
        public List<Comment> Comments { get; set; }
        public int User_Id { get; set; } // Uploader
    }
}

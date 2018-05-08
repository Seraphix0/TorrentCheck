using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;
using TorrentCheck.Models;

namespace TorrentCheck.DAL
{
    public class DataContext : DbContext
    {
        public DataContext() : base() { }

        public DbSet<Torrent> Torrents;
        public DbSet<File> Files;
        public DbSet<Comment> Comments;
        public DbSet<TorrentRating> TorrentRatings;
        public DbSet<UserRating> UserRatings;
        public DbSet<UserDownloads> UserDownloads;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TorrentCheck.Models;
using System.Net;
using System.IO;

namespace TorrentCheck.DAL
{
    public class TorrentContext : DbContext
    {
        public TorrentContext(DbContextOptions<DbContext> options) : base(options) { }

        public DbSet<Torrent> Torrents;
        public DbSet<Models.File> Files;
        public DbSet<Comment> Comments;
        public DbSet<TorrentRating> TorrentRatings;
        public DbSet<UserRating> UserRatings;
        public DbSet<UserDownloads> UserDownloads;
    }
}

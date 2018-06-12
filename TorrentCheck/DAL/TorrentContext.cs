using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TorrentCheck.Models;

namespace TorrentCheck.DAL
{
    public class TorrentContext : DbContext
    {
        public TorrentContext(DbContextOptions<TorrentContext> options) : base(options) { }

        public DbSet<Torrent> Torrent { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<Comment> Comment { get; set; }
        // public DbSet<TorrentRating> TorrentRating { get; set; }
        // public DbSet<UserRating> UserRating { get; set; }
        // public DbSet<UserDownloads> UserDownload { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Torrent>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<File>()
                .HasKey(i => i.Id);
            modelBuilder.Entity<File>()
                .HasOne(a => a.Torrent)
                .WithMany(b => b.Files);

            modelBuilder.Entity<Comment>()
                .HasKey(i => i.Id);
            modelBuilder.Entity<Comment>()
                .HasOne(a => a.Torrent)
                .WithMany(b => b.Comments);

            /*
            modelBuilder.Entity<UserRating>()
                .HasKey(c => new { c.UserName, c.RatedBy_UserName });

            modelBuilder.Entity<TorrentRating>()
                .HasKey(c => new { c.Torrent_Id, c.UserName });

            modelBuilder.Entity<UserDownloads>()
                .HasKey(c => new { c.Torrent_Id, c.UserName });
            */
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using TorrentCheck.Logic.Upload;
using Microsoft.EntityFrameworkCore;
using TorrentCheck.Models;
using TorrentCheck.DAL;
using BencodeNET.Torrents;
using System.Linq;

namespace TorrentCheck.Tests
{
    public class Upload
    {
        public Upload()
        {
            DbContextOptionsBuilder<TorrentContext> optionsBuilder = new DbContextOptionsBuilder<TorrentContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TorrentCheckLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            context = new TorrentContext(optionsBuilder.Options);
            logic = new UploadLogic(context);
            repository = new TorrentRepository(context);
            torrentFilePath = @"../../../config.torrent";
        }

        private readonly TorrentContext context;
        private readonly UploadLogic logic;
        private readonly ITorrentRepository repository;
        private readonly string torrentFilePath;

        [Fact]
        public void MagnetLink1()
        {
            BencodeNET.Torrents.Torrent decodedTorrent = logic.DecodeTorrentFile(torrentFilePath);
            Assert.Contains("magnet:", decodedTorrent.GetMagnetLink());
        }

        // Repository implementation test
        [Fact]
        public void Upload1()
        {
            BencodeNET.Torrents.Torrent torrent = logic.DecodeTorrentFile(torrentFilePath);
            logic.InsertTorrent(torrent, "TestName", torrentFilePath);
            IEnumerable<Models.Torrent> torrents = repository.GetAllTorrents();
            Assert.NotNull(torrents.Where(x => x.UserName == "TestName"));
        }
    }
}

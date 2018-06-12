using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TorrentCheck.Logic;
using System.IO;
using TorrentCheck.DAL;
using TorrentCheck.Models;
using Microsoft.EntityFrameworkCore;

namespace TorrentCheck.Tests
{
    public class Search
    {
        public Search()
        {
            DbContextOptionsBuilder<TorrentContext> optionsBuilder = new DbContextOptionsBuilder<TorrentContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TorrentCheckLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            context = new TorrentContext(optionsBuilder.Options);
            logicHTTP = new HomeLogicHTTP();
            logicSQL = new HomeLogicSQL(context);
        }

        private readonly TorrentContext context;
        private readonly HomeLogicHTTP logicHTTP;
        private readonly HomeLogicSQL logicSQL;

        // Offline source
        [Fact]
        public void SearchHTML1()
        {
            List<string> splitResults = logicHTTP.SplitResults(System.IO.File.ReadAllText(@"../../../ExampleQueryResult.html"));
            Assert.NotNull(logicHTTP.CompileList(splitResults));
        }

        // Online source
        [Fact]
        public void SearchHTML2()
        {
            Assert.NotNull(logicHTTP.GetResults("https://proxyfl.info/s/?q=game&page=0&orderby=99"));
        }

        // Repository implementation test
        [Fact]
        public void SearchSQL1()
        {
            Assert.NotEmpty(logicSQL.GetAllTorrents());
        }

        // Result formatting test
        [Fact]
        public void SearchSQL2()
        {
            List<Torrent> torrents = new List<Torrent>()
            {
                new Torrent(1,"Test",1234556,DateTime.Now,"A0B2C4D6","Melvin",Category.Applications,""),
                new Torrent(2,"XYZ",2454365,DateTime.MaxValue,"H3X4D3C1M4L","xUnit",Category.Games,""),
                new Torrent(3,"ABC",123,DateTime.MinValue,"ALPHA","Tester",Category.Porn,"")
            };

            Assert.NotEmpty(logicSQL.CompileList(torrents));
        }
    }
}

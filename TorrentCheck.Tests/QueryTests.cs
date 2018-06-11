using System;
using TorrentCheck.Models.HomeViewModels;
using Xunit;
using TorrentCheck.Logic;
using Microsoft.EntityFrameworkCore;
using TorrentCheck.DAL;

namespace TorrentCheck.Tests
{
    public class QueryTests
    {
        [Fact]
        public void SQLResultsTest1()
        {
            DbContextOptionsBuilder<DbContext> optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TorrentCheckLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            ITorrentRepository torrentRepository = new TorrentRepository(new TorrentContext(optionsBuilder.Options));

            HomeLogic logic = new HomeLogic(torrentRepository);

            SearchViewModel query = new SearchViewModel
            {
                Title = "bencode",
                Category = Models.Category.Undefined,
            };

            Assert.True(logic.GetResultsSQL(query) != null, "No results were returned.");
        }

        [Fact]
        public void SQLResultsTest2()
        {
            DbContextOptionsBuilder<DbContext> optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TorrentCheckLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            ITorrentRepository torrentRepository = new TorrentRepository(new TorrentContext(optionsBuilder.Options));

            HomeLogic logic = new HomeLogic(torrentRepository);

            SearchViewModel query = new SearchViewModel
            {
                Title = "xxx",
                Category = Models.Category.Undefined,
            };

            var Results = logic.GetResultsSQL(query);

            Assert.False(Results != null, "The method return unexpected results.");
        }
    }
}

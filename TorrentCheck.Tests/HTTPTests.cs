using System;
using System.Collections.Generic;
using System.Text;
using TorrentCheck.DAL;
using TorrentCheck.Logic;
using TorrentCheck.Models.HomeViewModels;
using Xunit;

namespace TorrentCheck.Tests
{
    public class HTTPTests
    {
        readonly ITorrentRepository repository;

        [Fact]
        public void HTTPTest1()
        {
            HomeLogic logic = new HomeLogic(new );

            SearchViewModel query = new SearchViewModel
            {
                Title = "bencode",
                Category = Models.Category.Undefined,
            };

            Assert.True(logic.GetResultsHTTP(query) != null, "No results were returned.");
        }
    }
}

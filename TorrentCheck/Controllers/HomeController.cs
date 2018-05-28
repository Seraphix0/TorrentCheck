using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using TorrentCheck.Models;
using TorrentCheck.Models.HomeViewModels;
using TorrentCheck.Logic;
using TorrentCheck.DAL;
using Microsoft.EntityFrameworkCore;

namespace TorrentCheck.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITorrentRepository torrentRepository;
        private readonly HomeLogic logic;

        public HomeController ()
        {
            DbContextOptionsBuilder<DbContext> optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TorrentCheckLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            torrentRepository = new TorrentRepository(new TorrentContext(optionsBuilder.Options));
            logic = new HomeLogic(torrentRepository);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Get list of qualifying torrents based on specified conditions.
        /// Sources used: HTTP('proxyfl.info'), SQL('MSSQLLocalDb')
        /// </summary>
        /// <param name="query">Model containing query params.</param>
        /// <returns>Results to View</returns>
        [HttpPost]
        public IActionResult Search(SearchViewModel query)
        {
            // Get results from HTTP source
            List<Result> HTTPResults = logic.GetResultsHTTP(query);

            // Get results from SQL Source
            List<Result> SQLResults = logic.GetResultsSQL(query);

            // Return results to View
            SearchViewModel searchViewModel = new SearchViewModel() { Title = query.Title, Category = query.Category, HTTPResults = HTTPResults, SQLResults = SQLResults };

            return View(searchViewModel);
        }

        public IActionResult Browse()
        {
            return View();
        }
    }
}

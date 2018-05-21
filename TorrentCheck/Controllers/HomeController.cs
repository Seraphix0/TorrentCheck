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

namespace TorrentCheck.Controllers
{
    public class HomeController : Controller
    {
        public HomeController ()
        {
            logic = new SearchLogic(new _SearchRepository(new SearchHTTPContext(), new SearchDbContext()));
        }

        private SearchLogic logic;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Search(SearchViewModel query)
        {
            // URI to read
            string uriString = String.Format("https://proxyfl.info/s/?q={0}&page=0&orderby=99", query.Search);

            // Execute query and return results
            string queryOutput = SearchLogic.SearchRepository.ExecuteQuery(uriString);

            // Split results and build haystack
            List<string> haystack = SplitResults(queryOutput);

            // Instantiate and populate data storage with filtered results
            List<Result> Results = new List<Result>();
            foreach (string element in haystack)
            {
                Results.Add(new Result(FilterTitle(element), FilterTrusted(element)));
            }

            ViewBag.Results = Results;
            return View();
        }
    }
}

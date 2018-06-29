using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using TorrentCheck.Models;
using TorrentCheck.Models.HomeViewModels;
using TorrentCheck.Logic;
using TorrentCheck.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TorrentCheck.Controllers
{
    public class HomeController : Controller
    {
        private readonly TorrentContext context;
        private readonly HomeLogic logic;

        public HomeController (TorrentContext context)
        {
            this.context = context;

            logic = new HomeLogic(context);
        }

        public IActionResult Index()
        {
            IEnumerable<SelectListItem> RemoteSources = new SelectList(logic.GetRemoteSources());
            return View(new SearchViewModel() { RemoteSources = RemoteSources });
        }

        public IActionResult Browse()
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
            List<Result> SQLResults = logic.GetResultsSQL(query);

            if (SQLResults != null)
            {
                query.SQLResults = SQLResults;
            }

            if (!query.ExcludeRemoteSources)
            {
                List<Result> HTTPResults = logic.GetResultsHTTP(query);
                if (HTTPResults != null)
                {
                    query.HTTPResults = HTTPResults;
                }
            }
            IEnumerable<SelectListItem> RemoteSources = new SelectList(logic.GetRemoteSources());
            query.RemoteSources = RemoteSources;

            return View(query);
        }

        public FileResult Download(string FilePath)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath);
            return File(fileBytes, "application/x-msdownload", Path.GetFileName(FilePath));
        }
    }
}

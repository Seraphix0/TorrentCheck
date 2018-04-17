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

namespace TorrentCheck.Controllers
{
    public class HomeController : Controller
    {
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
            string queryOutput = ExecuteQuery(uriString);

            // Split results and build haystack
            List<string> haystack = SplitResults(queryOutput);

            // Instantiate and populate data storage with filtered results
            List<Result> Results = new List<Result>();
            foreach (string element in haystack)
            {
                Results.Add(new Result(FilterTitle(element),FilterTrusted(element)));
            }

            ViewBag.Results = Results;
            return View();
        }

        public string ExecuteQuery(string uriString)
        {
            WebClient webClient = new WebClient();
            Stream stream = webClient.OpenRead(uriString);
            StreamReader sr = new StreamReader(stream);
            string content = sr.ReadToEnd();
            stream.Close();

            return content;
        }

        public List<string> SplitResults(string queryOutput)
        {
            string needle = "detName\">";
            string haystackHTML = queryOutput.Substring(queryOutput.IndexOf(needle));
            string[] haystackArray = haystackHTML.Split(needle);
            List<string> haystack = haystackArray.ToList();

            haystack.RemoveRange(0, 2);
            haystack.Remove(haystack.Last());

            return haystack;
        }

        public string FilterTitle(string element)
        {
            string needle1 = "</a>";
            string needle2 = "\">";
            List<string> Titles = new List<string>();

            string result = element.Substring(element.IndexOf(needle2) + 2);
            result = result.Substring(0, result.IndexOf(needle1));

            return result;
        }

        public bool FilterTrusted(string element)
        {
            string needle1 = "vip";
            string needle2 = "trusted";

            if (element.Contains(needle1) || element.Contains(needle2))
            {
                return true;
            }

            return false;
        }
    }
}

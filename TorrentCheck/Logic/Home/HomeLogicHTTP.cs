﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TorrentCheck.Models;
using static TorrentCheck.Models.Torrent;

namespace TorrentCheck.Logic
{
    public class HomeLogicHTTP
    {
        public List<Result> GetResults(string uriString)
        {
            string queryOutput = ExecuteQuery(uriString);
            List<string> splitResults = SplitResults(queryOutput);

            if (splitResults != null)
            {
                return CompileList(splitResults);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Execute query on remote web server and return page source.
        /// </summary>
        /// <param name="uriString">URI to GET.</param>
        /// <returns>HTML page source.</returns>
        public string ExecuteQuery(string uriString)
        {
            try
            {
                WebClient webClient = new WebClient();
                Stream stream = webClient.OpenRead(uriString);
                StreamReader sr = new StreamReader(stream);
                string content = sr.ReadToEnd();
                stream.Close();
                return content;
            }
            catch (WebException)
            {
                return null;
            }

            
        }

        /// <summary>
        /// Return list with split results.
        /// </summary>
        /// <param name="queryOutput">HTML page source haystack.</param>
        /// <returns>List with split results.</returns>
        public List<string> SplitResults(string queryOutput)
        {
            string needle = "detName\">";
            try
            {
                string haystackHTML = queryOutput.Substring(queryOutput.IndexOf(needle));
                string[] haystackArray = haystackHTML.Split(needle);
                List<string> haystack = haystackArray.ToList();

                haystack.RemoveRange(0, 2);
                haystack.Remove(haystack.Last());

                return haystack;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Return list with formatted results.
        /// </summary>
        /// <param name="splitResults">List with split results.</param>
        /// <returns>List with formatted results.</returns>
        public List<Result> CompileList(List<string> splitResults)
        {
            List<Result> results = new List<Result>();
            foreach (string element in splitResults)
            {
                results.Add(new Result(FilterTitle(element), FilterTrusted(element), FilterCategory(element), FilterLink(element)));
            }

            return results;
        }

        /// <summary>
        /// Return title for given result.
        /// </summary>
        /// <param name="element">Result to filter.</param>
        /// <returns>Title for given result.</returns>
        public string FilterTitle(string element)
        {
            string needle1 = "</a>";
            string needle2 = "\">";
            List<string> Titles = new List<string>();

            string result = element.Substring(element.IndexOf(needle2) + needle2.Length);
            return result.Substring(0, result.IndexOf(needle1));
        }

        /// <summary>
        /// Return trusted state for given result.
        /// </summary>
        /// <param name="element">Result to filter.</param>
        /// <returns>Trusted state for given result.</returns>
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

        /// <summary>
        /// Return category for given result.
        /// </summary>
        /// <param name="element">Result to filter.</param>
        /// <returns>Category for given result.</returns>
        public Category FilterCategory(string element)
        {
            string needle1 = "category\">";
            string needle2 = "</a><br>";

            string result = element.Substring(element.IndexOf(needle1) + needle1.Length);
            result = result.Substring(0, result.IndexOf(needle2));

            if (Enum.TryParse(result, out Category category))
            {
                return category;
            }

            return Category.Undefined;
        }

        /// <summary>
        /// Return link to external page for given result.
        /// </summary>
        /// <param name="element"></param>
        /// <returns>Link to external page for given result.</returns>
        public string FilterLink(string element)
        {
            string needle1 = "/torrent/";
            string needle2 = "\"";
            string extSource = "https://proxyfl.info";

            string result = element.Substring(element.IndexOf(needle1));
            result = result.Substring(0, result.IndexOf(needle2));

            return extSource + result;
        }
    }
}

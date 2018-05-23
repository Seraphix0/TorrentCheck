using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrentCheck.DAL;
using Microsoft.EntityFrameworkCore;
using TorrentCheck.Models;
using TorrentCheck.Models.HomeViewModels;

namespace TorrentCheck.Logic
{
    public class HomeLogic
    {
        private readonly ITorrentRepository repository;
        private readonly HomeLogicHTTP logicHTTP;
        private readonly HomeLogicSQL logicSQL;

        public HomeLogic(ITorrentRepository repository)
        {
            this.repository = repository;
            logicHTTP = new HomeLogicHTTP();
            logicSQL = new HomeLogicSQL();
        }

        /// <summary>
        /// Get list of results from HTTP source.
        /// </summary>
        /// <param name="query">Query conditions for which the search results have to qualify.</param>
        /// <returns>List of results from HTTP source.</returns>
        public List<Result> GetResultsHTTP (SearchViewModel query)
        {
            string uriString = String.Format("https://proxyfl.info/s/?q={0}&page=0&orderby=99", query.Title);
            string queryOutput = logicHTTP.ExecuteQuery(uriString);
            List<string> splitResults = logicHTTP.SplitResults(queryOutput);
            return logicHTTP.CompileList(splitResults);
        }

        /// <summary>
        /// Get list of results from SQL source.
        /// </summary>
        /// <param name="query">Query conditions for which the search results have to qualify.</param>
        /// <returns>List of results from SQL source.</returns>
        public List<Result> GetResultsSQL (SearchViewModel query)
        {
            IEnumerable<Torrent> torrents = repository.GetTorrents();
            try
            {
                return logicSQL.CompileList(torrents);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Database returned zero results.");
            }

            return null;
        }
    }
}


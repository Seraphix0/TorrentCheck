using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrentCheck.Models;

namespace TorrentCheck.Logic
{
    public class HomeLogicSQL
    {
        /// <summary>
        /// Return list with formatted results.
        /// </summary>
        /// <param name="torrents">List with torrents queried from database.</param>
        /// <returns>List with formatted results.</returns>
        public List<Result> CompileList(IEnumerable<Torrent> torrents)
        {
            List<Result> results = new List<Result>();
            foreach (Torrent element in torrents)
            {
                results.Add(new Result(element.Title, true, true, element.Id, element.Category));
            }

            return results;
        }
    }
}

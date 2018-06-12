using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrentCheck.DAL;
using TorrentCheck.Models;

namespace TorrentCheck.Logic
{
    public class HomeLogicSQL
    {
        private readonly ITorrentRepository repository;

        public HomeLogicSQL(ITorrentRepository repository)
        {
            this.repository = repository;
        }

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
                results.Add(new Result(element.Title, true, true, element.Id, element.Category, element.FilePath, repository.GetFiles(element).ToList()));

                /*
                if (element.Files != null)
                {
                    results.Add(new Result(element.Title, true, true, element.Id, element.Category, element.FilePath, element.Files.ToList()));
                }
                else
                {
                    // TODO: Fix single file data
                    results.Add(new Result(element.Title, true, true, element.Id, element.Category, element.FilePath, new List<File>() { new File() }));
                }
                */
            }

            return results;
        }
    }
}

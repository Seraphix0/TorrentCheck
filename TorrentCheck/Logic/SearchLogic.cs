using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrentCheck.DAL;
using Microsoft.EntityFrameworkCore;

namespace TorrentCheck.Logic
{
    public class SearchLogic
    {

        /*
        private SearchRepository repository;

        public SearchLogic(SearchRepository _repository)
        {
            repository = _repository;
        }

        public List<Result> GetResults

        */

        public SearchLogic()
        {
            DbContextOptionsBuilder<DbContext> optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TorrentCheckLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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
}

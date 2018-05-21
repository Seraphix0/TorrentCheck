using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace TorrentCheck.DAL
{
    public class SearchHTTPContext : ISearchRepository
    {
        public string ExecuteQuery(string uriString)
        {
            WebClient webClient = new WebClient();
            Stream stream = webClient.OpenRead(uriString);
            StreamReader sr = new StreamReader(stream);
            string content = sr.ReadToEnd();
            stream.Close();

            return content;
        }
    }
}

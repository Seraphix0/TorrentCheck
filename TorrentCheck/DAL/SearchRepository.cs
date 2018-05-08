using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.DAL
{
    public class SearchRepository
    {
        private ISearchContext httpContext;
        private ISearchContext sqlContext;

        public SearchRepository(ISearchContext _httpContext, ISearchContext _sqlContext)
        {
            httpContext = _httpContext;
            sqlContext = _sqlContext;
        }

        public string ExecuteQuery(string uriString)
        {
            return httpContext.ExecuteQuery(uriString);
        }
    }
}

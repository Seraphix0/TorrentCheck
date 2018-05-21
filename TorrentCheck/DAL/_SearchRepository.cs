using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.DAL
{
    public class _SearchRepository
    {
        private ISearchRepository httpContext;
        private ISearchRepository sqlContext;

        public _SearchRepository(ISearchRepository _httpContext, ISearchRepository _sqlContext)
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

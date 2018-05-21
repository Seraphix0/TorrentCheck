using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.DAL
{
    public interface _ISearchContext
    {
        string ExecuteQuery(string uriString);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrentCheck.Models;

namespace TorrentCheck.DAL
{
    public interface ISearchRepository : IDisposable
    {
        IEnumerable<Torrent> GetTorrents(string searchTerm);
        Torrent GetTorrentById(int id);
        void InsertTorrent(Torrent torrent);
        void DeleteTorrent(int id);
        void UpdateTorrent(Torrent torrent);
        void Save(); // Nog niet zeker hoe dit moet worden toegepast.

    }
}

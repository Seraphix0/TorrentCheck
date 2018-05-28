using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrentCheck.Models;

namespace TorrentCheck.DAL
{
    public interface ITorrentRepository : IDisposable
    {
        IEnumerable<Torrent> GetTorrents();
        Torrent GetTorrentById(int id);
        void InsertTorrent(Torrent torrent);
        void DeleteTorrent(int id);
        void UpdateTorrent(Torrent torrent);

        IEnumerable<File> GetFiles(Torrent torrent);
        File GetFileById(int id);
        void InsertFile(File file);
        void DeleteFile(int id);
        void UpdateFile(File file);

        void SaveChanges();
    }
}

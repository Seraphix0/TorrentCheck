using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrentCheck.Models;

namespace TorrentCheck.DAL
{
    public class TorrentRepository : ITorrentRepository, IDisposable
    {
        private TorrentContext context;
        
        public TorrentRepository(TorrentContext context)
        {
            this.context = context;
        }

        public IEnumerable<Torrent> GetTorrents(string searchTerm)
        {
            return context.Torrents.ToList();
        }

        public Torrent GetTorrentById (int id)
        {
            return context.Torrents.Find(id);
        }

        public void InsertTorrent(Torrent torrent)
        {
            context.Torrents.Add(torrent);
        }

        public void DeleteTorrent(int id)
        {
            Torrent torrent = context.Torrents.Find(id);
            context.Torrents.Remove(torrent);
        }

        public void UpdateTorrent(Torrent torrent)
        {
            context.Entry(torrent).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

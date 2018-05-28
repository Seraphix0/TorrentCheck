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

        public IEnumerable<Torrent> GetTorrents()
        {
            try
            {
                return context.Torrent.ToList();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("There are currently no entries present in the database.");
            }

            return null;
        }

        public Torrent GetTorrentById (int id)
        {
            return context.Torrent.Find(id);
        }

        public void InsertTorrent(Torrent torrent)
        {
            context.Torrent.Add(torrent);
        }

        public void DeleteTorrent(int id)
        {
            Torrent torrent = context.Torrent.Find(id);
            context.Torrent.Remove(torrent);
        }

        public void UpdateTorrent(Torrent torrent)
        {
            context.Entry(torrent).State = EntityState.Modified;
        }

        // TODO: GetFiles 'WHERE F.Torrent = torrent'
        public IEnumerable<File> GetFiles(Torrent torrent)
        {
            return context.File.Where(t => t.Torrent == torrent);
        }

        public File GetFileById(int id)
        {
            return context.File.Find(id);
        }

        public void InsertFile(File file)
        {
            context.File.Add(file);
        }

        public void DeleteFile(int id)
        {
            File file = context.File.Find(id);
            context.File.Remove(file);
        }

        public void UpdateFile(File file)
        {
            context.Entry(file).State = EntityState.Modified;
        }

        public void SaveChanges()
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

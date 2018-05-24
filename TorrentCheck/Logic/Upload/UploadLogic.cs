using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TorrentCheck.Models;
using TorrentCheck.DAL;

namespace TorrentCheck.Logic.Upload
{
    public class UploadLogic
    {
        private readonly ITorrentRepository torrentRepository;

        public UploadLogic(ITorrentRepository repository)
        {
            torrentRepository = repository;
        }

        public BencodeNET.Torrents.Torrent DecodeTorrentFile(string torrentPath)
        {
            BencodeNET.Parsing.BencodeParser parser = new BencodeNET.Parsing.BencodeParser();
            return parser.Parse<BencodeNET.Torrents.Torrent>(torrentPath);
        }

        public void InsertTorrent(BencodeNET.Torrents.Torrent decodedTorrent)
        {
            Torrent torrent = new Torrent(decodedTorrent.DisplayName)

            torrentRepository.InsertTorrent(torrent);
        }
    }
}

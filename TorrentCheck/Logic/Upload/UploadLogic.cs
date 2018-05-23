using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TorrentCheck.Logic.Upload
{
    public class UploadLogic
    {
        public BencodeNET.Torrents.Torrent DecodeTorrent(string torrentPath)
        {
            BencodeNET.Parsing.BencodeParser parser = new BencodeNET.Parsing.BencodeParser();
            return parser.Parse<BencodeNET.Torrents.Torrent>(torrentPath);
        }
    }
}

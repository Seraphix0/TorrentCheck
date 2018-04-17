using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace TorrentCheck.Controllers
{
    public class TorrentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            WebClient webClient = new WebClient();
            return View(); ;
        }

        public bool UploadTorrent() { return true; }

        public BencodeNET.Torrents.Torrent DecodeTorrent(string torrentPath)
        {
            BencodeNET.Parsing.BencodeParser parser = new BencodeNET.Parsing.BencodeParser();
            return parser.Parse<BencodeNET.Torrents.Torrent>(torrentPath);
        }
    }
}
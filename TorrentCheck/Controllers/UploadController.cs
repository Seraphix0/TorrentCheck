using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TorrentCheck.Models.UploadViewModels;
using System.IO;
using TorrentCheck.Logic.Upload;
using TorrentCheck.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TorrentCheck.Controllers
{
    public class UploadController : Controller
    {
        private readonly TorrentContext context;
        private readonly UploadLogic logic;

        public UploadController(TorrentContext context)
        {
            this.context = context;

            logic = new UploadLogic(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Browse()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(UploadViewModel uploadViewModel)
        {
            // Process file upload
            if (uploadViewModel.TorrentToDecode.FileName.EndsWith(".torrent"))
            {
                string TorrentFilePath = @"Data\TorrentFiles\" + uploadViewModel.TorrentToDecode.FileName;
                TorrentFilePath = Path.GetFullPath(TorrentFilePath);
                using (var stream = new FileStream(TorrentFilePath, FileMode.Create))
                {
                    await uploadViewModel.TorrentToDecode.CopyToAsync(stream);
                }

                BencodeNET.Torrents.Torrent torrent = logic.DecodeTorrentFile(TorrentFilePath);

                logic.InsertTorrent(torrent, User.Identity.Name, TorrentFilePath);

                return View("Index", uploadViewModel);
            }

            else
            {
                return View("Index", uploadViewModel);
            }
        }
    }
}
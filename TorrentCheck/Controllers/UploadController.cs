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
        private readonly ITorrentRepository torrentRepository;
        private readonly UploadLogic logic;

        public UploadController()
        {
            DbContextOptionsBuilder<DbContext> optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TorrentCheckLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            torrentRepository = new TorrentRepository(new TorrentContext(optionsBuilder.Options));
            logic = new UploadLogic(torrentRepository);
        }

        public IActionResult Index()
        {
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

                // Insert torrent into database
                logic.InsertTorrent(torrent, User.Identity.Name, TorrentFilePath);

                return Ok(new { TorrentFilePath });
            }

            string errorMessage = "File is not of type .torrent!";
            return Ok(new { errorMessage });
        }
    }
}
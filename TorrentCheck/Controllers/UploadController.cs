using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TorrentCheck.Models;
using TorrentCheck.Models.UploadViewModels;
using System.IO;
using Microsoft.AspNetCore.Http;
using TorrentCheck.Logic.Upload;
using TorrentCheck.DAL;
using Microsoft.EntityFrameworkCore;

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
                var tempFilePath = Path.GetTempFileName();
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await uploadViewModel.TorrentToDecode.CopyToAsync(stream);
                }

                BencodeNET.Torrents.Torrent torrent = logic.DecodeTorrentFile(tempFilePath);

                // TODO: Insert torrent into database
                logic.InsertTorrent(torrent);

                return Ok(new { tempFilePath });
            }

            string errorMessage = "File is not of type .torrent!";
            return Ok(new { errorMessage });
            

            


            
        }
    }
}
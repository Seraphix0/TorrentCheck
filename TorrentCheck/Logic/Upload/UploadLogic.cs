using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TorrentCheck.Models;
using TorrentCheck.DAL;
using Microsoft.AspNetCore.Identity;
using static TorrentCheck.Models.Torrent;

namespace TorrentCheck.Logic.Upload
{
    public class UploadLogic
    {
        private readonly ITorrentRepository torrentRepository;

        public UploadLogic(TorrentContext context)
        {
            torrentRepository = new TorrentRepository(context);
        }

        public BencodeNET.Torrents.Torrent DecodeTorrentFile(string torrentPath)
        {
            BencodeNET.Parsing.BencodeParser parser = new BencodeNET.Parsing.BencodeParser();
            return parser.Parse<BencodeNET.Torrents.Torrent>(torrentPath);
        }

        public void InsertTorrent(BencodeNET.Torrents.Torrent decodedTorrent, string userName, string filePath)
        {
            Torrent torrent = new Torrent(decodedTorrent.DisplayName, decodedTorrent.TotalSize, DateTime.Now, decodedTorrent.GetInfoHash(), filePath, userName, Category.Audio);
            ICollection<Models.File> Files = PopulateFileList(decodedTorrent.File, decodedTorrent.Files, torrent);
            torrentRepository.InsertTorrent(torrent);

            foreach (Models.File file in Files)
            {
                torrentRepository.InsertFile(file);
            }

            torrentRepository.SaveChanges();
        }

        public ICollection<Models.File> PopulateFileList(BencodeNET.Torrents.SingleFileInfo singleFileInfo, BencodeNET.Torrents.MultiFileInfoList multiFileInfos, Torrent torrent)
        {
            ICollection<Models.File> Files = new List<Models.File>();

            if (multiFileInfos != null)
            {
                foreach (BencodeNET.Torrents.MultiFileInfo file in multiFileInfos)
                {
                    List<string> Path = (List<string>)file.Path;
                    Files.Add(new Models.File(file.FileName, file.FileSize, file.Md5Sum, file.FullPath, torrent));
                }
            }
            else
            {
                Files.Add(new Models.File(singleFileInfo.FileName, singleFileInfo.FileSize, singleFileInfo.Md5Sum, null, torrent));
            }

            return Files;
        }
    }
}

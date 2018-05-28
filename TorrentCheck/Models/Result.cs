﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrentCheck.Models.HomeViewModels;
using static TorrentCheck.Models.Torrent;
using System.ComponentModel.DataAnnotations;

namespace TorrentCheck.Models
{
    public class Result
    {
        [Key]
        [Required]
        public string Title { get; }

        public bool Trusted { get; }

        public bool IsHomeSource { get; }

        public int TorrentId { get; }

        public Category Category { get; }

        /// <summary>
        /// Result signifying single formatted <see cref="Models.Torrent"/> for use in presentation layer.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="trusted"></param>
        /// <param name="isHomeSource">Result originating from TorrentCheck index.</param>
        /// <param name="torrentId">Associated TorrentId in database.</param>
        /// <param name="category"></param>
        public Result(string title, bool trusted, bool isHomeSource, int torrentId, Category category)
        {
            Title = title;
            Trusted = trusted;
            IsHomeSource = isHomeSource;
            TorrentId = torrentId;
            Category = category;
        }

        /// <summary>
        /// Result signifying single formatted HTTP query result for use in presentation layer.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="trusted"></param>
        /// <param name="category"></param>
        public Result(string title, bool trusted, Category category)
        {
            Title = title;
            Trusted = trusted;
            Category = category;
        }
    }
}

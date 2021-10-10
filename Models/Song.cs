using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicDotNet.Models
{
    public class Song
    {
        public int SongId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(0.01, 9999999)]
        public double Length { get; set; }
        //FK
        public int ArtistId { get; set; }
        public int AlbumId { get; set; }
        //Parent ref
        public Artist Artist { get; set; }
        public Album Album { get; set; }
    }
}

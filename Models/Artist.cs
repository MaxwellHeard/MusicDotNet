using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicDotNet.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        [Required]
        public string Name { get; set; }
        //FK
        public int AlbumId { get; set; }
        public int SongId { get; set; }
        //Child ref
        public List<Song> Songs { get; set; }
        public List<Album> Albums { get; set; }
    }
}

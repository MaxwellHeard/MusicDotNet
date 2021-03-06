using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicDotNet.Models
{
    public class Album
    {
        public int AlbumId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Review { get; set; }

        [Range(0.01, 9999999)]
        public double Length { get; set; }

        [Display(Name = "Album Art")]
        public string AlbumArt { get; set; }

        //FK
        public int ArtistId { get; set; }

        //Parent ref
        public Artist Artist { get; set; }

        //Child ref
        public List<Song> Songs { get; set; }
    }
}

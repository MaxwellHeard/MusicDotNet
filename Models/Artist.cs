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
        public string Bio { get; set; }

        //Child ref
        public List<Album> Albums { get; set; }
    }
}

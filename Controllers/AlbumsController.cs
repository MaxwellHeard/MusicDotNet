using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicDotNet.Controllers
{
    public class AlbumsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewAlbum(string album)
        {
            //if the album is empty redirect to index
            if (album == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.album = album;
            return View();
        }
    }
}

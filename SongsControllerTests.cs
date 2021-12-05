using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicDotNet.Controllers;
using MusicDotNet.Data;
using MusicDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicDotNetTests
{
    [TestClass]
    public class SongsControllerTests
    {
        private ApplicationDbContext _context;

        List<Song> songs = new List<Song>();

        SongsController controller;

        [TestInitialize]
        public void TestInitalize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            var Album = new Album
            {
                AlbumId = 6,
                Title = "Song1",
                Review = "Good",
                Length = 3.14,
                ArtistId = 7,
            };

            songs.Add(new Song { SongId = 1, Title = "Song1", Length = 3.14, TrackNo = 7, AlbumId = 6, Album = Album });
            songs.Add(new Song { SongId = 2, Title = "Song2", Length = 3.14, TrackNo = 6, AlbumId = 6, Album = Album });
            songs.Add(new Song { SongId = 3, Title = "Song3", Length = 3.14, TrackNo = 5, AlbumId = 6, Album = Album });

            foreach (var s in songs)
            {
                _context.Songs.Add(s);
            }

            _context.SaveChanges();

            controller = new SongsController(_context);
        }

        //Index
        [TestMethod]
        public void IndexLoadsCorrectView()
        {
            var result = (ViewResult)controller.Index().Result;

            Assert.AreEqual("Index", result.ViewName);
        }
        [TestMethod]
        public void IndexReturnsSongData()
        {
            var result = (ViewResult)controller.Index().Result;

            List<Song> model = (List<Song>)result.Model;

            CollectionAssert.AreEqual(songs.OrderBy(s => s.Album.Title).ThenBy(s => s.TrackNo).ToList(), model);
        }

        //Details
        [TestMethod]
        public void DetailsNoId()
        {
            var result = controller.Details(id: null);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("404", viewResult.ViewName);
        }
        [TestMethod]
        public void DetailsInvalidId()
        {
            var result = controller.Details(id: -1);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("404", viewResult.ViewName);
        }
        [TestMethod]
        public void DetailsViewLoads()
        {
            int songId = songs[0].SongId;

            var result = controller.Details(songId);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Details", viewResult.ViewName);
        }
        [TestMethod]
        public void DetailSongReturnsMatches()
        {
            int id = songs[0].SongId;

            var result = controller.Details(id);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual(songs[0], viewResult.Model);
        }

        //Create
        [TestMethod]
        public void CreateLoadsView()
        {
            var result = controller.Create();
            var viewResult = (ViewResult)result;

            Assert.AreEqual("Create", viewResult.ViewName);
        }
        [TestMethod]
        public void CreateReturnsValidList()
        {
            var result = controller.Create();
            var viewData = controller.ViewData["AlbumId"];

            Assert.IsNotNull(viewData);
        }
        [TestMethod]
        public void CreateSavesToDb()
        {
            var song = new Song { SongId = 4, Title = "Song4", Length = 3.14, TrackNo = 4, AlbumId = 7 };
            _context.Songs.Add(song);
            _context.SaveChanges();

            Assert.AreEqual(song, _context.Songs.ToArray()[3]);
        }
        [TestMethod]
        public void CreateReturnsCreate()
        {
            var song = new Song { };
            controller.ModelState.AddModelError("songId", "songId");
            var result = controller.Create(song);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Create", viewResult.ViewName);
        }

        //Edit
        [TestMethod]
        public void EditNullId()
        {
            var result = controller.Edit(null);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("404", viewResult.ViewName);
        }
        [TestMethod]
        public void EditInvalidId()
        {
            var result = controller.Edit(-1);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("404", viewResult.ViewName);
        }
        [TestMethod]
        public void EditValidId()
        {
            var result = controller.Edit(1);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Edit", viewResult.ViewName);
        }
        [TestMethod]
        public void EditLoadsCorrectModel()
        {
            var result = controller.Edit(1);
            var viewResult = (ViewResult)result.Result;
            Song model = (Song)viewResult.Model;

            Assert.AreEqual(_context.Songs.Find(1), model);
        }
        [TestMethod]
        public void EditLoadsViewData()
        {
            var result = controller.Edit(1);
            var viewResult = (ViewResult)result.Result;
            var viewData = viewResult.ViewData;

            Assert.AreEqual(viewData, viewResult.ViewData);
        }
        [TestMethod]
        public void EditInvalidModel()
        {
            var result = controller.Edit(10);
            var viewResult = (ViewResult)result.Result;
            Song model = (Song)viewResult.Model;

            Assert.AreNotEqual(_context.Songs.FindAsync(1), model);
        }
        [TestMethod]
        public void EditSave()
        {
            var song = songs[0];
            song.TrackNo = 1;
            var result = controller.Edit(song.SongId, song);
            var redirectResult = (RedirectToActionResult)result.Result;

            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        //Delete
        [TestMethod]
        public void DeleteNullId()
        {
            var result = controller.Delete(null);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }
        [TestMethod]
        public void DeleteIdNotExists()
        {
            var result = controller.Delete(200);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }
        [TestMethod]
        public void DeleteCorrectView()
        {
            var id = 1;
            var result = controller.Delete(id);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Delete", viewResult.ViewName);
        }
        [TestMethod]
        public void DeleteCorrectSong()
        {
            var id = 1;
            var result = controller.Delete(id);
            var viewResult = (ViewResult)result.Result;
            Song song = (Song)viewResult.Model;

            Assert.AreEqual(songs[0], song);
        }
        [TestMethod]
        public void DeleteSuccess()
        {
            var id = 1;
            var result = controller.DeleteConfirmed(id); 
            var song = _context.Songs.Find(id);

            Assert.AreEqual(song, null);
        }
    }
}



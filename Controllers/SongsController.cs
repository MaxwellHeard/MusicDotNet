using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicDotNet.Data;
using MusicDotNet.Models;

namespace MusicDotNet.Controllers
{
    [Authorize]
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Songs
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Songs.Include(s => s.Album).OrderBy(s => s.Album.Title).ThenBy(s => s.TrackNo);
            return View("Index", await applicationDbContext.ToListAsync());
        }

        // GET: Songs/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("404");
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (song == null)
            {
                return View("404");
            }

            return View("Details", song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albums.OrderBy(a => a.Title), "AlbumId", "Title");
            return View("Create");
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SongId,Title,Length,TrackNo,AlbumId")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "Title", song.AlbumId);
            return View("Create", song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("404");
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return View("404");
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums.OrderBy(a => a.Title), "AlbumId", "Title", song.AlbumId);
            return View("Edit", song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SongId,Title,Length,TrackNo,AlbumId")] Song song)
        {
            if (id != song.SongId)
            {
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.SongId))
                    {
                        return View("404");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "Title", song.AlbumId);
            return View("Edit", song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (song == null)
            {
                return View("Error");
            }

            return View("Delete", song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.SongId == id);
        }
    }
}

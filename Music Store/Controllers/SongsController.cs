using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Music_Store.Data;
using Music_Store.Models;
using Music_Store.Models.ViewModels;
using Music_Store.Services;

namespace Music_Store.Controllers
{
    public class SongsController : Controller
    {
        private const string BASE_PATH_SAVE = "~/res/newImages/songs/";

        private readonly ApplicationDbContext _context;
        private readonly ISongsService _service;
        private readonly string basePath;
        
        public SongsController(ApplicationDbContext context, ISongsService service, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _service = service;

            string rootPath = webHostEnvironment.WebRootPath;
            basePath = Path.Combine(rootPath, "res", "newImages", "songs");
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Songs.Include(s => s.Album).Include(s => s.Artist).Include(s => s.Publisher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Songs/5/Details
        [Route("Songs/{id}/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songVm = await _service.GetSongOrNullAsync(id.Value);

            if (songVm == null)
            {
                return NotFound();
            }

            return View(songVm);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["AlbumName"] = new SelectList(_context.Albums, "ID", "Name");
            ViewData["ArtistFullname"] = new SelectList(_context.Artists, "ID", "Fullname");
            ViewData["PublisherName"] = new SelectList(_context.Publishers, "ID", "Name");
            ViewData["GenreName"] = _context.Genres.Select(g => new SelectListItem(g.Name, g.Name)).ToList();


            var songVm = new SongViewModel
            {
                Genres = new List<string>()
            };
            return View(songVm);
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Genres,ID,ArtistID,AlbumID,PublisherID,ImageFile,Name,ReleaseDate,RuntimeInSec,Rating,FavouriteCount,PurchaseCount,Price,DataUrl")] SongViewModel songVm,
            Image image)
        {
            if (!ModelState.IsValid)
            {
                return View(songVm);
            }

            string extension = Path.GetExtension(songVm.ImageFile.FileName);
            string fileName = songVm.Name + extension;
            string path = Path.Combine(basePath, fileName);

            // find
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await image.ImageFile.CopyToAsync(fileStream);
            }

            // TODO: put data in it
            Song song = new Song
            {
                ArtistID = songVm.ArtistID,
                AlbumID = songVm.AlbumID,
                PublisherID = songVm.PublisherID,
                Name = songVm.Name,
                ReleaseDate = songVm.ReleaseDate,
                RuntimeInSec = songVm.RuntimeInSec,
                FavouriteCount = songVm.FavouriteCount,
                PurchaseCount = songVm.PurchaseCount,
                DataUrl = songVm.DataUrl,
                Price = songVm.Price,
                ImagePath = BASE_PATH_SAVE + fileName
            };

            // genre
            foreach(var genreName in songVm.Genres)
            {
                if(!string.IsNullOrEmpty(genreName))
                {
                    var songGenre = await _context.Genres
                        .Where(g => g.Name == genreName)
                        .Select(g => new SongGenre 
                        { 
                            Genre = g
                        })
                        .FirstOrDefaultAsync();

                    song.SongGenres.Add(songGenre);
                }
            }
            _context.Add(song);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            
            if (song == null)
            {
                return NotFound();
            }

            
            var songVm = new SongViewModel
            {
                ID=song.ID,
                ArtistID = song.ArtistID,
                AlbumID = song.AlbumID,
                PublisherID = song.PublisherID,
                Name = song.Name,
                ReleaseDate = song.ReleaseDate,
                RuntimeInSec = song.RuntimeInSec,
                FavouriteCount = song.FavouriteCount,
                PurchaseCount = song.PurchaseCount,
                DataUrl = song.DataUrl,
                Price = song.Price,
                ImagePath = song.ImagePath
            };

            foreach (var genre in song.SongGenres)
            {
                if (genre.ID != 0)
                {
                    var songGenre = await _context.Genres
                        .Where(g => g.ID == genre.ID)
                        .Select(g => new SongGenre
                        {
                            Genre = g
                        })
                        .FirstOrDefaultAsync();

                    songVm.Genres.Add(songGenre.Genre.Name);
                }
            }

            ViewData["AlbumName"] = new SelectList(_context.Albums, "ID", "Name", songVm.AlbumID);
            ViewData["ArtistFullname"] = new SelectList(_context.Artists, "ID", "Fullname", songVm.ArtistID);
            ViewData["PublisherName"] = new SelectList(_context.Publishers, "ID", "Name", songVm.PublisherID);
            ViewData["GenreName"] = _context.Genres.Select(g => new SelectListItem(g.Name, g.Name)).ToList();
            
            return View(songVm);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Genres,ID,ArtistID,AlbumID,PublisherID,ImageFile, ImagePath,Name,ReleaseDate,RuntimeInSec,Rating,FavouriteCount,PurchaseCount,Price,DataUrl")] SongViewModel songVm,
            Image image)
        {
            
            ViewData["AlbumName"] = new SelectList(_context.Albums, "ID", "Name", songVm.AlbumID);
            ViewData["ArtistFullname"] = new SelectList(_context.Artists, "ID", "Fullname", songVm.ArtistID);
            ViewData["PublisherName"] = new SelectList(_context.Publishers, "ID", "Name", songVm.PublisherID);
            string extension = "";
            string fileName = "";
            if(songVm.ImageFile != null)
            {
                string path = Path.Combine(basePath, fileName);
                extension = Path.GetExtension(songVm.ImageFile.FileName);
                fileName = songVm.Name + extension;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await image.ImageFile.CopyToAsync(fileStream);
                }
            }
            else
            {
                fileName = songVm.Name + ".png";
            }
            

            // find
           

            Song newSong = new Song
            {
                ID = songVm.ID,
                ArtistID = songVm.ArtistID,
                AlbumID = songVm.AlbumID,
                PublisherID = songVm.PublisherID,
                Name = songVm.Name,
                ReleaseDate = songVm.ReleaseDate,
                RuntimeInSec = songVm.RuntimeInSec,
                FavouriteCount = songVm.FavouriteCount,
                PurchaseCount = songVm.PurchaseCount,
                DataUrl = songVm.DataUrl,
                Price = songVm.Price,
                ImagePath = BASE_PATH_SAVE + fileName
            };
            if (id != songVm.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newSong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(songVm.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(newSong);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .Include(s => s.Artist)
                .Include(s => s.Publisher)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
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
            return _context.Songs.Any(e => e.ID == id);
        }      
    }
}

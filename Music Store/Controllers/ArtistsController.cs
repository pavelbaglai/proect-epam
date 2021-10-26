using System;
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
    public class ArtistsController : Controller
    {
        private const string BASE_PATH_SAVE = "~/res/newImages/artists/";

        private readonly ApplicationDbContext _context;
        private readonly IArtistsService _service;
        private readonly string basePath;

        public ArtistsController(ApplicationDbContext context, IArtistsService service, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _service = service;
            string rootPath = webHostEnvironment.WebRootPath;
            basePath = Path.Combine(rootPath, "res", "newImages", "artists");
        }

        // GET: Artists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Artists.ToListAsync());
        }

        // GET: Artists/5/Details
        [Route("Artists/{id}/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistVm = await _service.GetArtistOrNullAsync(id.Value);
            if (artistVm == null)
            {
                return NotFound();
            }

            return View(artistVm);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StageName,FullName,ImagePath,ImageFile,DebutYear")] ArtistViewModel artistVm,
            Image image)
        {
            string extension = Path.GetExtension(artistVm.ImageFile.FileName);
            string fileName = artistVm.FullName + extension;
            string path = Path.Combine(basePath, fileName);

            // find
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await image.ImageFile.CopyToAsync(fileStream);
            }
            Artist artist = new Artist
            {
                Stagename = artistVm.StageName,
                Fullname = artistVm.FullName,
                DebutYear = artistVm.DebutYear,
                ImagePath = BASE_PATH_SAVE + fileName
            };
            if (ModelState.IsValid)
            {
                _context.Add(artist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artistVm);
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            var artistVm = new ArtistViewModel
            {
                ID = artist.ID,
                StageName = artist.Stagename,
                FullName = artist.Fullname,
                ImagePath = artist.ImagePath,
                DebutYear = artist.DebutYear
            };
            return View(artistVm);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Stagename,Fullname,ImagePath,ImageFile, DebutYear")] ArtistViewModel artistVm)
        {
            string extension = "";
            string fileName = "";
            if (artistVm.ImageFile != null)
            {
                string path = Path.Combine(basePath, fileName);
                extension = Path.GetExtension(artistVm.ImageFile.FileName);
                fileName = artistVm.FullName + extension;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await artistVm.ImageFile.CopyToAsync(fileStream);
                }
            }
            else
            {
                fileName = artistVm.FullName + ".png";
            }
            if (id != artistVm.ID)
            {
                return NotFound();
            }
            Artist updatedArtist = new Artist
            {
                ID = artistVm.ID,
                Stagename = artistVm.StageName,
                Fullname = artistVm.FullName,
                DebutYear = artistVm.DebutYear,
                ImagePath = BASE_PATH_SAVE + fileName
            };

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedArtist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artistVm.ID))
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
            return View(updatedArtist);
        }

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .FirstOrDefaultAsync(m => m.ID == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.ID == id);
        }
    }
}

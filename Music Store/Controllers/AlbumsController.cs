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
    public class AlbumsController : Controller
    {
        private const string BASE_PATH_SAVE = "~/res/newImages/albums/";
        private readonly ApplicationDbContext _context;
        private readonly IAlbumsService _service;
        private readonly string basePath;

        public AlbumsController(ApplicationDbContext context, IAlbumsService service, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _service = service;
            string rootPath = webHostEnvironment.WebRootPath;
            basePath = Path.Combine(rootPath, "res", "newImages", "albums");
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Albums.Include(a => a.Artist).Include(a => a.Publisher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Albums/5/Details
        [Route("Albums/{id}/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumVm = await _service.GetAlbumOrNullAsync(id.Value);
            if (albumVm == null)
            {
                return NotFound();
            }

            return View(albumVm);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewData["ArtistFullname"] = new SelectList(_context.Artists, "ID", "Fullname");
            ViewData["PublisherName"] = new SelectList(_context.Publishers, "ID", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PublisherID,ArtistID,Name,ImagePath,ImageFile,PublishDate,Rating,FavouriteCount,PurchaseCount,Price")] AlbumViewModel albumVm,
            Image image)
        {
            
            ViewData["ArtistFullname"] = new SelectList(_context.Artists, "ID", "Fullname");
            ViewData["PublisherName"] = new SelectList(_context.Publishers, "ID", "Name");
            string extension = Path.GetExtension(albumVm.ImageFile.FileName);
            string fileName = albumVm.Name + extension;
            string path = Path.Combine(basePath, fileName);

            // find
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await image.ImageFile.CopyToAsync(fileStream);
            }

            Album album = new Album
            {
                ArtistID = albumVm.ArtistID,
                PublisherID = albumVm.PublisherID,
                Name = albumVm.Name,
                ImagePath = BASE_PATH_SAVE + fileName,
                PublishDate = albumVm.PublishDate,
                FavouriteCount = albumVm.FavouriteCount,
                PurchaseCount = albumVm.PurchaseCount,
                Price = albumVm.Price
            };
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(albumVm);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            var albumVm = new AlbumViewModel
            {
                ArtistID = album.ArtistID,
                PublisherID = album.PublisherID,
                Name = album.Name,
                ImagePath = album.ImagePath,
                PublishDate = album.PublishDate,
                FavouriteCount = album.FavouriteCount,
                PurchaseCount = album.PurchaseCount,
                Price = album.Price
            };
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ID", "Fullname", album.ArtistID);
            ViewData["PublisherID"] = new SelectList(_context.Publishers, "ID", "Name", album.PublisherID);
            return View(albumVm);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PublisherID,ArtistID,Name,ImagePath,ImageFile,PublishDate,Rating,FavouriteCount,PurchaseCount,Price")] AlbumViewModel albumVm,
            Image image)
        {
            string extension = "";
            string fileName = "";
            if (albumVm.ImageFile != null)
            {
                string path = Path.Combine(basePath, fileName);
                extension = Path.GetExtension(albumVm.ImageFile.FileName);
                fileName = albumVm.Name + extension;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await image.ImageFile.CopyToAsync(fileStream);
                }
            }
            else
            {
                fileName = albumVm.Name + ".png";
            }

            Album album = new Album
            {
                ArtistID = albumVm.ArtistID,
                PublisherID = albumVm.PublisherID,
                Name = albumVm.Name,
                ImagePath = BASE_PATH_SAVE + fileName,
                PublishDate = albumVm.PublishDate,
                FavouriteCount = albumVm.FavouriteCount,
                PurchaseCount = albumVm.PurchaseCount,
                Price = albumVm.Price
            };
            if (id != album.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.ID))
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
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ID", "Fullname", album.ArtistID);
            ViewData["PublisherID"] = new SelectList(_context.Publishers, "ID", "Name", album.PublisherID);
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Publisher)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.ID == id);
        }
    }
}

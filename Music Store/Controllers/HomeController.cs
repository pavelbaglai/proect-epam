using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Music_Store.Data;
using Music_Store.Models;
using Music_Store.Models.ViewModels;
using Music_Store.Services;

namespace Music_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISearchService _service;

        public HomeController(ApplicationDbContext context, ISearchService service)
        {
            _context = context;
            _service = service;
        }
        /*
        public IActionResult Index()
        {
            return View();
        }
        */


        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Songs.Include(s => s.Album).Include(s => s.Artist).Include(s => s.Publisher);

            return View(await applicationDbContext.ToListAsync());
        }
        
        public async Task<IActionResult> Search(string searchString)
        {
            if (searchString == null || searchString == "")
            {
                return RedirectToAction("Index", "Home");
            }

            var results = await _service.GetSearchResult(searchString);
            
            return View(results);
        }

        public async Task<IActionResult> Category(string category)
        {
            CategoryViewModel model = new CategoryViewModel();

            model.SongList = new List<Song>();
            model.ArtistList = new List<Artist>();

            foreach (var item in _context.Artists)
            {
                model.ArtistList.Add(item);
            }

            // TODO: We don't really have a way to differentiate music by category right now
            switch (category)
            {
                case "hot100":
                    model.Category = "Hot 100";
                    foreach (var item in _context.Songs)
                    {
                        model.SongList.Add(item);
                    }
                    break;
                case "trending":
                    model.Category = "Trending Now";
                    foreach (var item in _context.Songs)
                    {
                        model.SongList.Add(item);
                    }
                    break;
                case "upandcoming":
                    model.Category = "Up & Coming";
                    foreach (var item in _context.Songs)
                    {
                        model.SongList.Add(item);
                    }
                    break;
                default:
                    return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

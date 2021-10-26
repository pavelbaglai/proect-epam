using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Music_Store.Data;
using Music_Store.Helpers;
using Music_Store.Models;
using Music_Store.Models.ViewModels;
using Music_Store.Services;

namespace Music_Store.Controllers
{
    [Authorize(Roles = nameof(Customer))]
    public class CheckoutController : Controller
    {
        private const string SESSION_KEY = "music_store";

        private readonly ApplicationDbContext _context;
        private readonly ICartsService _cartService;
        private readonly UserManager<User> _userManager;

        private const string BASE_PATH = "~/res/songs/";

        private const string Dot = ".";
        private const string DefaultMimeType = "application/octet-stream";
        private static readonly Lazy<IDictionary<string, string>> _mappings = new Lazy<IDictionary<string, string>>(BuildMappings);

        public CheckoutController(ApplicationDbContext context, ICartsService cartService, UserManager<User> userManager)
        {
            _context = context;
            _cartService = cartService;
            _userManager = userManager;
        }

        //GET: order list
        public async Task<IActionResult> Index()
        {
            
            User user = await _userManager.GetUserAsync(User);
            var customerID = user.CustomerID.Value;
            var invoiceVms = _context.Invoices.Where(i => i.CustomerID == customerID).Select(invoice => new InvoiceViewModel
            {
                ID = invoice.ID,
                CustomerID = invoice.CustomerID,
                CreatedDate = invoice.CreatedDate,
                TotalPrice = @Math.Round(invoice.InvoiceDetails.Sum(id => id.Price), 2, MidpointRounding.AwayFromZero),
                Items = invoice.InvoiceDetails.Select(id => id.SongID.HasValue ?
                new InvoiceDetailViewModel
                {
                    ID = id.ID,
                    ItemID = id.SongID.Value,
                    Category = nameof(Song),
                    ItemName = id.Song.Name,
                    ImagePath = id.Song.ImagePath,
                    ArtistName = id.Song.Artist.Stagename,
                    Price = id.Song.Price
                }
                :
                new InvoiceDetailViewModel
                {
                    ID = id.ID,
                    ItemID = id.AlbumID.Value,
                    Category = nameof(Album),
                    ItemName = id.Album.Name,
                    ImagePath = id.Album.ImagePath,
                    ArtistName = id.Album.Artist.Stagename,
                    Price = id.Album.Price
                }).ToList()
            }).AsEnumerable();

            //InvoiceViewModel invoiceVm = new InvoiceViewModel();
            //var invoices = _context.Invoices.Include(i => i.InvoiceDetails).Select(i => i.CustomerID == customerID);
            //foreach (Invoice i in invoices)
            //{

            //    invoiceVm.ID = i.ID;
            //    invoiceVm.CustomerID = i.CustomerID;
            //    invoiceVm.CreatedDate = i.CreatedDate;
            //    invoiceVm.TotalPrice = @Math.Round(i.InvoiceDetails.Sum(id => id.Price), 2, MidpointRounding.AwayFromZero);
            //    invoiceVm.Items = i.InvoiceDetails.Select(id => id.SongID.HasValue ?
            //    new InvoiceDetailViewModel
            //    {
            //        ID = id.ID,
            //        ItemID = id.SongID.Value,
            //        Category = nameof(Song),
            //        ItemName = id.Song.Name,
            //        ImagePath = id.Song.ImagePath,
            //        ArtistName = id.Song.Artist.Stagename,
            //        Price = id.Song.Price
            //    }
            //    :
            //    new InvoiceDetailViewModel
            //    {
            //        ID = id.ID,
            //        ItemID = id.AlbumID.Value,
            //        Category = nameof(Album),
            //        ItemName = id.Album.Name,
            //        ImagePath = id.Album.ImagePath,
            //        ArtistName = id.Album.Artist.Stagename,
            //        Price = id.Album.Price
            //    }).ToList();
            //}

           
            return View(invoiceVms);
        }

        public async Task<IActionResult> Success(int invoiceID)
        {
            var invoiceVm = _context.Invoices.Where(i => i.ID == invoiceID).Select(invoice => new InvoiceViewModel
            {
                ID = invoice.ID,
                CustomerID = invoice.CustomerID,
                CreatedDate = invoice.CreatedDate,
                TotalPrice = @Math.Round(invoice.InvoiceDetails.Sum(id => id.Price), 2, MidpointRounding.AwayFromZero),
                Items = invoice.InvoiceDetails.Select(id => id.SongID.HasValue ?
                new InvoiceDetailViewModel
                {
                    ID = id.ID,
                    ItemID = id.SongID.Value,
                    Category = nameof(Song),
                    ItemName = id.Song.Name,
                    ImagePath = id.Song.ImagePath,
                    ArtistName = id.Song.Artist.Stagename,
                    Price = id.Song.Price
                }
                :
                new InvoiceDetailViewModel
                {
                    ID = id.ID,
                    ItemID = id.AlbumID.Value,
                    Category = nameof(Album),
                    ItemName = id.Album.Name,
                    ImagePath = id.Album.ImagePath,
                    ArtistName = id.Album.Artist.Stagename,
                    Price = id.Album.Price
                }).ToList()
            }).FirstOrDefault();

            return View(invoiceVm);
        }

        // GET: Checkout
        public async Task<IActionResult> Create()
        {
            CartViewModel cartVm;
            ViewResult viewResult;

            string previousPage = ((string)TempData["previousPage"]).ToLower();
            var user = await _userManager.GetUserAsync(User);
            var customerID = user.CustomerID.Value;
            switch (previousPage)
            {
                case "cart":                    
                    cartVm = await _cartService.GetCartFromCustomerAsync(customerID);
                    viewResult = View(cartVm);
                    break;
                case "login":
                    cartVm = await _cartService.GetCartFromSessionAsync(HttpContext, SESSION_KEY);
                    viewResult = View(cartVm);
                    break;
                default:
                    viewResult = View("Error", new ErrorViewModel());
                    break;
            }

            //var creditCards = _context.Customers.Where(i => i.ID == customerID).Select(i => i.CreditCards);
            var creditCards = _context.CreditCards.Where(i => i.CustomerID == customerID);
            var list = new SelectList(creditCards, "ID", "CreditCardNumber");
            ViewData["CreditCard"] = new SelectList(creditCards, "ID", "CreditCardNumber");

            return viewResult;
        }

        // POST: Checkout/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int customerID, [FromForm] CartViewModel cartVm)
        {
            try
            {
                if (customerID == 0)
                {
                    User user = await _userManager.GetUserAsync(User);
                    customerID = user.CustomerID.Value;
                }

                Invoice invoice = new Invoice
                {
                    CustomerID = customerID
                };

                foreach (var cartItem in cartVm.Items)
                {
                    int? songID = null;
                    int? albumID = null;

                    switch (cartItem.Category)
                    {
                        case nameof(Song):
                            songID = cartItem.ItemID;
                            break;
                        case nameof(Album):
                            albumID = cartItem.ItemID;
                            break;
                        default:
                            break;
                    }

                    invoice.AddInvoiceDetail(songID, albumID, cartItem.Price);
                }

                await _context.AddAsync(invoice);


                var page = (string)TempData["previousPage"];
                if (page.Equals("checkoutFromCart", StringComparison.OrdinalIgnoreCase))
                {
                    var cartItems = _context.Customers.Where(c => c.ID == customerID).SelectMany(c => c.Cart.CartItems);
                    _context.RemoveRange(cartItems);
                }
                else
                {
                    HttpContext.Session.Remove(SESSION_KEY);
                }

               

                await _context.SaveChangesAsync();

                return RedirectToAction("Success", new { invoiceID = invoice.ID });
            }
            catch (Exception e)
            {
                return RedirectToAction("Create");
            }
        }


        private static IDictionary<string, string> BuildMappings()
        {
            var mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
            #region Big freaking list of mime types
            {".mp3","audio/mpeg"},
            {".png","image/png" }
                #endregion
            };
            var cache = mappings.ToList(); // need ToList() to avoid modifying while still enumerating

            foreach (var mapping in cache)
            {
                if (!mappings.ContainsKey(mapping.Value))
                {
                    mappings.Add(mapping.Value, mapping.Key);
                }
            }

            return mappings;
        }
        //private Dictionary<string, string> GetMimeType()
        //{
        //    return new Dictionary<string, string>
        //    {
        //        {".mp3","audio/mpeg"},
        //        {".png","image/png" }
        //    };
        //}
        public static string GetMimeType(string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException(nameof(extension));
            }

            if (!extension.StartsWith(Dot))
            {
                extension = Dot + extension;
            }

            return _mappings.Value.TryGetValue(extension, out string mime) ? mime : DefaultMimeType;
        }
        public static string GetExtension(string mimeType, bool throwErrorIfNotFound = true)
        {
            if (mimeType == null)
            {
                throw new ArgumentNullException(nameof(mimeType));
            }

            if (mimeType.StartsWith("."))
            {
                throw new ArgumentException("Requested mime type is not valid: " + mimeType);
            }

            if (_mappings.Value.TryGetValue(mimeType, out string extension))
            {
                return extension;
            }

            if (throwErrorIfNotFound)
            {
                throw new ArgumentException("Requested mime type is not registered: " + mimeType);
            }

            return string.Empty;
        }
        public async Task<IActionResult> Download(int? id, string category)
        {
            if (id == null)
            {
                return NotFound();
            }


            if (category.Equals("Song", StringComparison.OrdinalIgnoreCase))
            {
                var song = await _context.Songs.FindAsync(id);
                var path = song.DataUrl;
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                var ext = Path.GetExtension(path).ToLowerInvariant();
                return File(memory, GetMimeType(ext), Path.GetFileName(path));
            }
            else
            {
                var albumn = await _context.Albums.FindAsync(id);

                using (FileStream zipToOpen = new FileStream($"{albumn.Name}.zip", FileMode.Open))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                    {
                        foreach(Song song in albumn.Songs)
                        {
                            ZipArchiveEntry readmeEntry = archive.CreateEntry(song.Name);
                        }
                    }
                    return File(zipToOpen, "application/zip", $"{albumn.Name}.zip");
                }                
            }
        }
    }
}

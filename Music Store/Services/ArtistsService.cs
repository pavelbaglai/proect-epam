using Microsoft.EntityFrameworkCore;
using Music_Store.Data;
using Music_Store.Models.ViewModels;
using Music_Store.QueryObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Services
{
    public class ArtistsService : IArtistsService
    {
        private readonly ApplicationDbContext _context;

        public ArtistsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ArtistViewModel> GetArtistOrNullAsync(int id)
        {
            var artistViewModel = await _context.Artists
                .AsNoTracking()
                .Where(a => a.ID == id)
                .MapArtistToVM()
                .FirstOrDefaultAsync();

            if (artistViewModel != null)
            {
                artistViewModel.Genres = artistViewModel.Genres.Distinct();
            }

            return artistViewModel;
        }

        public async Task<IEnumerable<ArtistViewModel>> GetArtistsAsync()
        {
            var artistViewModels = await _context.Artists
                .AsNoTracking()
                .MapArtistToVM()
                .ToListAsync();

            artistViewModels.ForEach(vm => vm.Genres = vm.Genres.Distinct());

            return artistViewModels;
        }
    }
}

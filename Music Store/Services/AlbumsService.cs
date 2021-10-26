using Microsoft.EntityFrameworkCore;
using Music_Store.Data;
using Music_Store.Models.ViewModels;
using Music_Store.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Services
{
    public class AlbumsService : IAlbumsService
    {
        private readonly ApplicationDbContext _context;

        public AlbumsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AlbumViewModel> GetAlbumOrNullAsync(int id)
        {
            var albumViewModel = await _context.Albums
                .AsNoTracking()
                .Where(a => a.ID == id)
                .MapAlbumToVM()
                .FirstOrDefaultAsync();
            
            if(albumViewModel != null)
            {
                albumViewModel.Genres = albumViewModel.Genres.Distinct();
            }

            return albumViewModel;
        }

        public async Task<IEnumerable<AlbumViewModel>> GetAlbumsAsync()
        {
            var albumViewModels = await _context.Albums
                .AsNoTracking()
                .MapAlbumToVM()
                .ToListAsync();
            
            albumViewModels.ForEach(vm => vm.Genres = vm.Genres.Distinct());

            return albumViewModels;
        }
    }
}

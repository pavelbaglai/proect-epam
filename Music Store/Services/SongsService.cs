using Microsoft.EntityFrameworkCore;
using Music_Store.Data;
using Music_Store.Models.ViewModels;
using Music_Store.QueryObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Services
{
    public class SongsService : ISongsService
    {
        private readonly ApplicationDbContext _context;

        public SongsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SongViewModel> GetSongOrNullAsync(int id)
        {
            return await _context.Songs
                .AsNoTracking()
                .Where(s => s.ID == id)
                .MapSongToVM()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SongViewModel>> GetSongsAsync()
        {
            return await _context.Songs
                .AsNoTracking()
                .MapSongToVM()
                .ToListAsync();
        }
    }
}

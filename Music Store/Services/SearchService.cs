using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Music_Store.Data;
using Music_Store.Models.ViewModels;
using Music_Store.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Services
{
    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext _context;

        public SearchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SearchResultViewModel> GetSearchResult(string searchString)
        {
            SearchResultViewModel result = new SearchResultViewModel();
            result.SearchString = searchString;
            result.SongList = await _context.Songs
                .AsNoTracking()
                .Where(n => n.Name.ToLower().Contains(searchString.ToLower()))
                .MapSongToVM()
                .ToListAsync();
            result.ArtistList = await _context.Artists
                .AsNoTracking()
                .Where(n => n.Stagename.ToLower().Contains(searchString.ToLower()))
                .MapArtistToVM()
                .ToListAsync();
            result.AlbumList = await _context.Albums
                .AsNoTracking()
                .Where(n => n.Name.ToLower().Contains(searchString.ToLower()))
                .MapAlbumToVM()
                .ToListAsync();
            result.SongFoundCount = result.SongList.Count();
            result.ArtistFoundCount = result.ArtistList.Count();
            result.AlbumFoundCount = result.AlbumList.Count();

            return result;
            //throw new NotImplementedException();
        }
    }
}

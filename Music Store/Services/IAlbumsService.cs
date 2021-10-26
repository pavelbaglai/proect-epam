using Music_Store.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Services
{
    public interface IAlbumsService
    {
        Task<AlbumViewModel> GetAlbumOrNullAsync(int id);
        Task<IEnumerable<AlbumViewModel>> GetAlbumsAsync();
    }
}

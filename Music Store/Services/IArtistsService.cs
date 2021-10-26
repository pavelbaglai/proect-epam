using Music_Store.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Music_Store.Services
{
    public interface IArtistsService
    {
        Task<ArtistViewModel> GetArtistOrNullAsync(int id);
        Task<IEnumerable<ArtistViewModel>> GetArtistsAsync();
    }
}

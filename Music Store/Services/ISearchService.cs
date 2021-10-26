using Music_Store.Models.ViewModels;
using System.Threading.Tasks;

namespace Music_Store.Services
{
    public interface ISearchService
    {
        Task<SearchResultViewModel> GetSearchResult(string searchString);
    }
}

using OpenFoodFacts.Models.Wrappers;

namespace OpenFoodFacts.Services.Interfaces
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}

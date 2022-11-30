using GetCat.Application.DTO;

namespace GetCat.Application.Services.Interfaces
{
    public interface ICatService
    {
        Task<IList<CatsDto>> Get(int limit, int page);
    }
}

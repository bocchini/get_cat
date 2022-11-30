using Newtonsoft.Json;
using GetCat.Application.DTO;
using GetCat.Application.Wrapper;
using GetCat.Application.Services.Interfaces;

namespace GetCat.Application.Services
{
    public class CatService : ICatService
    {
        private readonly string url = "https://api.thecatapi.com/v1/images/";
        private readonly IHttpClientWrapper _httpClientWrapper;

        public CatService(IHttpClientWrapper httpClientWrapper)
        {
            this._httpClientWrapper = httpClientWrapper;
        }
        public async Task<IList<CatsDto>> Get(int limit, int page)
        {
            var response = await _httpClientWrapper.GetAsync(url + $"search?limit={limit}0&page={page}");

            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                if(!string.IsNullOrEmpty(result))
                {
                    return JsonConvert.DeserializeObject<IList<CatsDto>>(result).ToList();
                }
            }
           return new List<CatsDto>();
        }
    }
}

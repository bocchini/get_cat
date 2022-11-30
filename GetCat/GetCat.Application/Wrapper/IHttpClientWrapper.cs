namespace GetCat.Application.Wrapper;

public interface IHttpClientWrapper
{
    Task<HttpResponseMessage> GetAsync(string url);
}

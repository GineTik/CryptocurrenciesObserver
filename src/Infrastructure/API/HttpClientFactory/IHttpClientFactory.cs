using System.Net.Http;

namespace Infrastructure.API.HttpClientFactory;

public interface IHttpClientFactory
{
    void SetBaseUri(string uri);
    HttpClient Create();
}
using System.Net.Http;

namespace Infrastructure.API;

public interface IHttpClientFactory
{
    void SetBaseUri(string uri);
    HttpClient Create();
}
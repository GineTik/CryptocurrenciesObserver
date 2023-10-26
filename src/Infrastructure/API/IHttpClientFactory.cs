using System.Net.Http;

namespace Infrastructure.API;

public interface IHttpClientFactory
{
    HttpClient Create();
}
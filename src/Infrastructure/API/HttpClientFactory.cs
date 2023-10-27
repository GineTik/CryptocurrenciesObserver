using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Infrastructure.API;

public class HttpClientFactory : IHttpClientFactory
{
    private string? _baseUri;
    
    public void SetBaseUri(string uri)
    {
        _baseUri = uri;
    }

    public HttpClient Create()
    {
        var client = new HttpClient();
        if (_baseUri != null)
        {
            client.BaseAddress = new Uri(_baseUri);
        }
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return client;
    }
}
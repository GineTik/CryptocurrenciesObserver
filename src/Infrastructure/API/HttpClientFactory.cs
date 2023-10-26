using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.API;

public class HttpClientFactory
{
    private readonly IConfiguration _configuration;

    public HttpClientFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public HttpClient Create()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://pro-api.coingecko.com/api/v3");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("x-cg-pro-api-key", _configuration["ApiKey"]);
        return client;
    }
}
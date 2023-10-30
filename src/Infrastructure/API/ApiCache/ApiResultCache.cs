using System;
using System.Threading.Tasks;
using Domain.Results;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.API.ApiCache;

public class ApiResultCache : IApiResultCache
{
    private readonly IMemoryCache _memoryCache;
    private readonly IConfiguration _configuration;

    public ApiResultCache(IMemoryCache memoryCache, IConfiguration configuration)
    {
        _memoryCache = memoryCache;
        _configuration = configuration;
    }

    public async Task<ApiResult<TContent>> Cache<TContent>(object key, Func<Task<ApiResult<TContent>>> setter)
        where TContent : class
    {
        if (_memoryCache.TryGetValue(key, out ApiResult<TContent>? cachedApiResult))
            return cachedApiResult!;
        
        var result = await setter();
        if (!result.Successfully) 
            return result;
        
        var absoluteExpirationMinutes = Convert.ToDouble(_configuration["AbsoluteExpirationToCacheInMinutes"]);
        cachedApiResult = _memoryCache.Set(key, result, TimeSpan.FromMinutes(absoluteExpirationMinutes));
        
        return cachedApiResult;
    }
}
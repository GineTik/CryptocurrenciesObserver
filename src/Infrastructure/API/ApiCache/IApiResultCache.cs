using System;
using System.Threading.Tasks;
using Domain.Results;

namespace Infrastructure.API.ApiCache;

public interface IApiResultCache
{
    Task<ApiResult<TContent>> Cache<TContent>(object key, Func<Task<ApiResult<TContent>>> setter)
        where TContent : class;
}
using System;
using System.Collections.Generic;
using System.Net.Http;
using Domain.APIModels;

namespace Domain.Results;

public record ApiResult<TContent>(TContent Content, Exception? Exception = null) 
    : RequestResult<TContent>(Content, Exception)
    where TContent : class;

public static class ApiResult
{
    public static ApiResult<TContent> Content<TContent>(TContent content) where TContent : class =>
        new ApiResult<TContent>(content);

    public static ApiResult<TContent> Exception<TContent>(Exception exception) where TContent : class =>
        new ApiResult<TContent>(null!, exception);

    public static ApiResult<TContent> Exception<TContent>(HttpResponseMessage responseMessage) where TContent : class =>
        Exception<TContent>(new HttpRequestException(
            $"The response (request {responseMessage.RequestMessage!.RequestUri}) is unsuccessful\n",
            null,
            responseMessage.StatusCode));
}
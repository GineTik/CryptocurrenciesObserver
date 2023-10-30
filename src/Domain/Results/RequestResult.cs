using System;

namespace Domain.Results;

public record RequestResult<TResult>(TResult Content, Exception? Exception)
    where TResult : class
{
    public RequestResult(TResult result) : this(result, null) {}
    public RequestResult(Exception exception) : this(null!, exception) {}

    public bool Successfully => Exception == null;
};
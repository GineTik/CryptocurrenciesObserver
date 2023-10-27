using System;

namespace Domain.Results;

public record QueryResult<TResult>(TResult Content, Exception? Exception)
    where TResult : class
{
    public QueryResult(TResult result) : this(result, null) {}
    public QueryResult(Exception exception) : this(null!, exception) {}

    public bool Successfully => Exception == null;
};
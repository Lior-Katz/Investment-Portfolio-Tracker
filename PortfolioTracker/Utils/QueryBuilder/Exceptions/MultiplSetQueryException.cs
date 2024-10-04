using System;

namespace PortfolioTracker.Utils.QueryBuilder.Exceptions;

public class MultipleSetQueryException : InvalidOperationException
{
    public MultipleSetQueryException(string paramName, string message = "cannot set multiple values")
        : base($" Illegal set of {paramName}, {message}")
    { }
}

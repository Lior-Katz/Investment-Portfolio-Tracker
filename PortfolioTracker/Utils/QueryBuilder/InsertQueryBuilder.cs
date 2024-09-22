using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using PortfolioTracker.Utils.QueryBuilder.Exceptions;

namespace PortfolioTracker.Utils.QueryBuilder;

public class InsertQueryBuilder : QueryBuilderBase
{
    private Dictionary<string, object>? _params = null;
    private List<string>? _outputColumns = null;

    public InsertQueryBuilder Into(string? tableName)
    {
        SetTableName(tableName);
        return this;
    }

    public InsertQueryBuilder Connection(SqlConnection? connection)
    {
        SetConnection(connection);
        return this;
    }

    public InsertQueryBuilder Params(Dictionary<string, object>? @params)
    {
        if (@params == null || @params.Count == 0)
        {
            return this;
        }

        if (_params == null)
        {
            _params = new Dictionary<string, object>();
        }

        foreach (var (key, value) in @params)
        {
            if (!_params!.TryAdd(key, value))
            {
                throw new MultipleSetQueryException("params", $"cannot set multiple params to key {key}");
            }
        }

        return this;
    }

    public InsertQueryBuilder Output(List<string>? outputColumns)
    {
        if (outputColumns == null || outputColumns.Count == 0)
        {
            return this;
        }

        if (_outputColumns == null)
        {
            _outputColumns = new List<string>();
        }

        _outputColumns.AddRange(outputColumns);
        return this;
    }

    public override string Build()
    {
        if (string.IsNullOrEmpty(TableName))
        {
            throw new InvalidOperationException("Table name is not set");
        }

        if (_params == null || _params.Count == 0)
        {
            throw new InvalidOperationException("Params are not set");
        }

        StringBuilder builder = new StringBuilder();
        builder.Append("INSERT INTO ");
        builder.Append(TableName);
        builder.Append(" (");
        builder.Append(string.Join(", ", _params.Keys));
        builder.Append(") VALUES (");
        builder.Append(string.Join(", ", _params.Values));
        builder.Append(")");
        if (_outputColumns != null && _outputColumns.Count > 0)
        {
            builder.Append(" OUTPUT ");
            builder.Append(string.Join(", ", _outputColumns));
        }
        return builder.ToString();
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Extensions.Primitives;
using PortfolioTracker.Utils.QueryBuilder.Exceptions;

namespace PortfolioTracker.Utils.QueryBuilder;

public class 
    InsertQueryBuilder : QueryBuilderBase
{
    private List<string>? _outputColumns;
    private List<Dictionary<string, object>>? _params;
    private HashSet<String>? _paramsKeys;

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
            _params = new List<Dictionary<string, object>>();
        }

        if (_paramsKeys == null)
        {
            _paramsKeys = new HashSet<string>();
        }

        _paramsKeys.UnionWith(@params.Keys);

        // add a copy of @params to _params
        _params.Add(@params.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));

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

        if (_paramsKeys == null || _paramsKeys.Count == 0 || _params == null || _params.Count == 0)
        {
            throw new InvalidOperationException("Params are not set");
        }

        // surround all string values with single quotes
        // foreach (Dictionary<string, object> paramsDict in _params)
        // {
        //     foreach (var (key, value) in paramsDict)
        //     {
        //         if (value is string)
        //         {
        //             paramsDict[key] = $"'{value}'";
        //         }
        //     }
        // }

        var builder = new StringBuilder();
        builder.Append("INSERT INTO ");
        builder.Append(TableName);
        // foreach (Dictionary<string, object> paramsDict in _params)
        // {
        //     foreach (var (key, value) in paramsDict)
        //     {
        //         if (value is string)
        //         {
        //             paramsDict[key] = $"'{value}'";
        //         }
        //     }
        // }

        builder.Append(" (");

        builder.Append(string.Join(", ", _paramsKeys));

        builder.Append(")");
        if (_outputColumns != null && _outputColumns.Count > 0)
        {
            builder.Append(" OUTPUT ");
            builder.Append(string.Join(", ", _outputColumns));
        }

        builder.Append(" VALUES ");
        builder.Append(String.Join(", ", _params.Where(paramsDict => paramsDict.Count > 0).Select(paramsDict =>
                                                    BuildValues(paramsDict)
                                       )));

        // builder.Append(string.Join(", ", _params.Values));
        // builder.Append(")");

        return builder.ToString();
    }

    private string BuildValues(Dictionary<string, object> paramsDict)
    {
        StringBuilder builder = new StringBuilder();
        var updatedParams = paramsDict.ToDictionary(
                                                    kvp => kvp.Key,
                                                    kvp => kvp.Value is string ? $"'{kvp.Value}'" : kvp.Value
                                                   );
        return "(" + String.Join(", ", _paramsKeys!.Select(key => updatedParams!.GetValueOrDefault(key, null))) + ")";
    }
}

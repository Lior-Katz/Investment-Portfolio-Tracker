﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PortfolioTracker.Utils.QueryBuilder.Exceptions;

namespace PortfolioTracker.Utils.QueryBuilder;

public class SqlCommandBuilder
{
    private List<string>? _columns;
    private QueryCommand? _command;
    private SqlConnection? _connection;
    private List<string>? _outputColumns;
    private Dictionary<string, object>? _params;
    private SelectQueryBuilder.SearchPredicate? _searchPredicate;
    private string? _tableName;

    public SqlCommandBuilder Connection(SqlConnection connection)
    {
        if (_connection != null)
        {
            throw new MultipleSetQueryException("connection");
        }

        _connection = connection;
        return this;
    }

    public SqlCommandBuilder Table(string tableName)
    {
        if (string.IsNullOrEmpty(tableName))
        {
            throw new ArgumentException("Table name cannot be null or empty");
        }

        if (_tableName != null)
        {
            throw new MultipleSetQueryException("table name");
        }

        _tableName = _tableName;
        return this;
    }

    public SqlCommandBuilder SetCommand(QueryCommand command)
    {
        if (_command != null)
        {
            throw new MultipleSetQueryException("command");
        }

        _command = command;
        return this;
    }

    public SqlCommandBuilder Params(Dictionary<string, object>? @params)
    {
        if (@params == null || @params.Count == 0)
        {
            return this;
        }

        if (_params == null)
        {
            _params = new Dictionary<string, object>();
        }

        foreach (var item in @params)
        {
            if (!_params!.TryAdd(item.Key, item.Value))
            {
                throw new MultipleSetQueryException("params", $"cannot set multiple params to key {item.Key}");
            }
        }

        return this;
    }

    public SqlCommandBuilder Columns(List<string>? columns)
    {
        if (columns == null || columns.Count == 0)
        {
            return this;
        }

        if (_columns == null)
        {
            _columns = new List<string>();
        }

        _columns.AddRange(columns);
        return this;
    }

    public SqlCommandBuilder Condition(SelectQueryBuilder.SearchPredicate? searchPredicate)
    {
        if (searchPredicate == null)
        {
            return this;
        }

        if (_searchPredicate != null)
        {
            throw new MultipleSetQueryException("search condition");
        }

        _searchPredicate = searchPredicate;
        return this;
    }

    public SqlCommandBuilder Output(List<string>? outputColumns)
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

    public SelectQueryBuilder Select(List<string> columns)
    {
        Columns(columns);
        var builder = new SelectQueryBuilder();
        if (_tableName != null)
        {
            builder.From(_tableName);
        }

        if (_searchPredicate != null)
        {
            builder.Where(_searchPredicate);
        }

        if (_columns != null)
        {
            builder.Columns(_columns);
        }

        if (_connection != null)
        {
            builder.Connection(_connection);
        }

        return builder;
    }

    public InsertQueryBuilder Insert(Dictionary<string, object> @params)
    {
        Params(@params);
        var builder = new InsertQueryBuilder();
        if (_tableName != null)
        {
            builder.Into(_tableName);
        }

        if (_params != null)
        {
            builder.Params(_params);
        }

        if (_outputColumns != null)
        {
            builder.Output(_outputColumns);
        }

        if (_connection != null)
        {
            builder.SetConnection(_connection);
        }

        return builder;
    }


    public SqlCommand Build()
    {
        var query = "";
        switch (_command)
        {
            case QueryCommand.SELECT:
                query = BuildSelect();
                break;
            case QueryCommand.INSERT:
                query = BuildInsert();
                break;
            case QueryCommand.UPDATE:
                throw new NotSupportedException();
            case QueryCommand.DELETE:
                throw new NotSupportedException();
            default:
                throw new InvalidOperationException("Invalid command");
        }

        return new SqlCommand(query, _connection);
    }

    private string BuildInsert()
    {
        return new InsertQueryBuilder().Connection(_connection).Into(_tableName).Params(_params).Output(_outputColumns)
                                       .Build();
    }

    private string BuildSelect()
    {
        return new SelectQueryBuilder().Connection(_connection).From(_tableName).Columns(_columns)
                                       .Where(_searchPredicate).Build();
    }
}

public enum QueryCommand
{
    SELECT,
    INSERT,
    UPDATE,
    DELETE
}

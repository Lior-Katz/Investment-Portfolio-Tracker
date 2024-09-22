using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using PortfolioTracker.Utils.QueryBuilder.Exceptions;

namespace PortfolioTracker.Utils.QueryBuilder;

public class SelectQueryBuilder : QueryBuilderBase
{
    private List<string>? _columns;
    private SearchPredicate? _searchPredicate;
    private Dictionary<string, object> _values = null;

    public SelectQueryBuilder Connection(SqlConnection? connection)
    {
        SetConnection(connection);
        return this;
    }

    public SelectQueryBuilder From(string? tableName)
    {
        SetTableName(tableName);
        return this;
    }

    public SelectQueryBuilder Where(SearchPredicate? searchPredicate)
    {
        if (searchPredicate == null) return this;

        if (_searchPredicate != null) throw new MultipleSetQueryException("search condition");

        _searchPredicate = searchPredicate;
        return this;
    }

    public SelectQueryBuilder Columns(List<string>? columns)
    {
        if (columns == null || columns.Count == 0) return this;

        if (_columns == null) _columns = new List<string>();

        _columns.AddRange(columns);
        return this;
    }

    public override string Build()
    {
        var query = new StringBuilder();
        query.Append("SELECT ");
        var desiredColumns = _columns != null ? string.Join(", ", _columns) : "ALL";
        query.Append(desiredColumns);
        query.Append(" FROM ");
        query.Append(TableName);
        if (_searchPredicate != null)
        {
            query.Append(" WHERE ");
            query.Append(_searchPredicate?.Build());
        }

        return query.ToString();
    }

    public class SearchPredicate
    {
        private readonly string? _column;
        private readonly List<(string Operator, SearchPredicate Predicate)> _conditions = new();
        private readonly QueryConditionalOperator? _operator;
        private readonly object? _value;


        public SearchPredicate(string column, QueryOperator op, string value)
        {
            _column = column;
            _operator = new QueryConditionalOperator(op);
            _value = value;
        }

        // public SearchPredicate Column(string column)
        // {
        //     _column = column;
        //     return this;
        // }
        //
        // public SearchPredicate Operator(QueryConditionalOperator op)
        // {
        //     _operator = op;
        //     return this;
        // }
        //
        // public SearchPredicate Value(object value)
        // {
        //     _value = value;
        //     return this;
        // }

        public SearchPredicate And(SearchPredicate other)
        {
            _conditions.Add(("AND", other));
            return this;
        }

        public SearchPredicate And(string column, QueryOperator op, string value)
        {
            return And(new SearchPredicate(column, op, value));
        }

        public SearchPredicate Or(SearchPredicate other)
        {
            _conditions.Add(("OR", other));
            return this;
        }

        public SearchPredicate Or(string column, QueryOperator op, string value)
        {
            return Or(new SearchPredicate(column, op, value));
        }

        public string Build()
        {
            var builder = new StringBuilder();
            builder.Append($"{_column} {_operator} {_value}");
            foreach (var (op, predicate) in _conditions) builder.Append($" {op} {predicate.Build()}");
            return builder.ToString();
        }
    }
}

public class QueryConditionalOperator
{
    private readonly QueryOperator _operator;

    public QueryConditionalOperator(QueryOperator op)
    {
        _operator = op;
    }

    public QueryConditionalOperator(QueryConditionalOperator other)
    {
        _operator = other._operator;
    }

    public override string ToString()
    {
        switch (_operator)
        {
            case QueryOperator.LIKE:
                return "LIKE";
            case QueryOperator.IN:
                return "IN";
            case QueryOperator.EQUALS:
                return "=";
            case QueryOperator.NOT_EQUALS:
                return "!=";
            case QueryOperator.GREATER_THAN:
                return ">";
            case QueryOperator.LESS_THAN:
                return "<";
            case QueryOperator.GREATER_THAN_OR_EQUAL:
                return ">=";
            case QueryOperator.LESS_THAN_OR_EQUAL:
                return "<=";
            default:
                throw new InvalidOperationException("Invalid operator");
        }
    }
}

public enum QueryOperator
{
    LIKE,
    IN,
    EQUALS,
    NOT_EQUALS,
    GREATER_THAN,
    LESS_THAN,
    GREATER_THAN_OR_EQUAL,
    LESS_THAN_OR_EQUAL
}

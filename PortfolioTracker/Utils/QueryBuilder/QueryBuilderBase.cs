using System;
using System.Data.SqlClient;
using PortfolioTracker.Utils.QueryBuilder.Exceptions;

namespace PortfolioTracker.Utils.QueryBuilder;

public abstract class QueryBuilderBase
{
    protected SqlConnection? SqlConnection;
    protected string? TableName;

    public void SetConnection(SqlConnection? connection)
    {
        if (SqlConnection != null) throw new MultipleSetQueryException("connection");

        SqlConnection = connection ?? throw new ArgumentNullException("Connection cannot be null");
    }

    public void SetTableName(string? tableName)
    {
        if (string.IsNullOrEmpty(tableName)) throw new ArgumentException("Table name cannot be null or empty");

        if (TableName != null) throw new MultipleSetQueryException("table name");

        TableName = tableName;
    }

    public abstract string Build();

    public SqlCommand BuildCommand()
    {
        if (SqlConnection != null) SqlConnection.Open();
        return new SqlCommand(Build(), SqlConnection);
    }
}

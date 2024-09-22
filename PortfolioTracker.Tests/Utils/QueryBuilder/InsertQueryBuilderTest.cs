using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortfolioTracker.Utils.QueryBuilder;
using PortfolioTracker.Utils.QueryBuilder.Exceptions;

namespace PortfolioTracker.Tests.Utils.QueryBuilder;

[TestClass]
public class InsertQueryBuilderTest
{
    [TestMethod]
    public void InsertQueryWithParamsAndOutput()
    {
        var builder = new InsertQueryBuilder();
        var query = builder
                    .Into("Users")
                    .Params(new Dictionary<string, object>
                            { { "Username", "'exampleUser'" }, { "Password", "'examplePassword'" } })
                    .Output(new List<string> { "INSERTED.Id" })
                    .Build();
        Assert.AreEqual("INSERT INTO Users (Username, Password) VALUES ('exampleUser', 'examplePassword') OUTPUT INSERTED.Id",
                        query);
    }

    [TestMethod]
    public void InsertQueryWithParamsOnly()
    {
        var builder = new InsertQueryBuilder();
        var query = builder
                    .Into("Users")
                    .Params(new Dictionary<string, object>
                            { { "Username", "'exampleUser'" }, { "Password", "'examplePassword'" } })
                    .Build();
        Assert.AreEqual("INSERT INTO Users (Username, Password) VALUES ('exampleUser', 'examplePassword')", query);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void InsertQueryWithoutTableName()
    {
        var builder = new InsertQueryBuilder();
        builder
            .Params(new Dictionary<string, object>
                    { { "Username", "'exampleUser'" }, { "Password", "'examplePassword'" } })
            .Build();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void InsertQueryWithoutParams()
    {
        var builder = new InsertQueryBuilder();
        builder
            .Into("Users")
            .Build();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void InsertQueryWithDuplicateParams()
    {
        var builder = new InsertQueryBuilder();
        builder
            .Into("Users")
            .Params(new Dictionary<string, object>
                    { { "Username", "'exampleUser'" }, { "Username", "'duplicateUser'" } })
            .Build();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void InsertQueryWithNullParams()
    {
        var builder = new InsertQueryBuilder();
        var query = builder
                    .Into("Users")
                    .Params(null)
                    .Build();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void InsertQueryWithEmptyParams()
    {
        var builder = new InsertQueryBuilder();
        var query = builder
                    .Into("Users")
                    .Params(new Dictionary<string, object>())
                    .Build();
    }

    [TestMethod]
    public void InsertQueryWithNullOutput()
    {
        var builder = new InsertQueryBuilder();
        var query = builder
                    .Into("Users")
                    .Params(new Dictionary<string, object>
                            { { "Username", "'exampleUser'" }, { "Password", "'examplePassword'" } })
                    .Output(null)
                    .Build();
        Assert.AreEqual("INSERT INTO Users (Username, Password) VALUES ('exampleUser', 'examplePassword')", query);
    }

    [TestMethod]
    public void InsertQueryWithEmptyOutput()
    {
        var builder = new InsertQueryBuilder();
        var query = builder
                    .Into("Users")
                    .Params(new Dictionary<string, object>
                            { { "Username", "'exampleUser'" }, { "Password", "'examplePassword'" } })
                    .Output(new List<string>())
                    .Build();
        Assert.AreEqual("INSERT INTO Users (Username, Password) VALUES ('exampleUser', 'examplePassword')", query);
    }
}

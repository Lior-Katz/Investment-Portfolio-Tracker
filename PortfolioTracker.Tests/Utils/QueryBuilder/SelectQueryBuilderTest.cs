using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortfolioTracker.Utils.QueryBuilder;

namespace PortfolioTracker.Tests.Utils.QueryBuilder;

[TestClass]
public class SelectQueryBuilderTest
{
    [TestMethod]
    [DataRow("table", new[] { "column1", "column2" },
             new[] { "column1 = value1", "column1 = value AND column2 <= value2" })]
    public void SelectQueryTest(string tableName, string[] columns, string[] conditions)
    {
        var builder = new SelectQueryBuilder();

        var expected = $"SELECT {string.Join(", ", columns)} FROM {tableName}";
        var actual = builder.From(tableName).Columns(columns.ToList()).Build();
        Assert.AreEqual(expected, actual);

        builder = new SelectQueryBuilder();
        expected = $"SELECT {string.Join(", ", columns)} FROM {tableName} WHERE {conditions[0]}";
        actual = builder.Columns(columns.ToList()).From(tableName)
                        .Where(new SelectQueryBuilder.SearchPredicate(columns[0], QueryOperator.EQUALS, "value1"))
                        .Build();
        Assert.AreEqual(expected, actual);

        builder = new SelectQueryBuilder();
        expected = $"SELECT {string.Join(", ", columns)} FROM {tableName} WHERE {conditions[1]}";
        actual = builder.Columns(columns.ToList()).From(tableName)
                        .Where(new SelectQueryBuilder.SearchPredicate(columns[0], QueryOperator.EQUALS, "value")
                                   .And(columns[1], QueryOperator.LESS_THAN_OR_EQUAL, "value2")).Build();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("table", new[] { "column1", "column2" }, "column1 = value1",
             "SELECT column1, column2 FROM table WHERE column1 = value1")]
    public void SelectQueryWithConditions(string tableName, string[] columns, string condition, string expected)
    {
        var builder = new SelectQueryBuilder();
        var actual = builder.From(tableName).Columns(columns.ToList())
                            .Where(new SelectQueryBuilder.SearchPredicate(columns[0], QueryOperator.EQUALS,
                                                                          "value1")).Build();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("table", new[] { "column1", "column2" }, "SELECT column1, column2 FROM table")]
    [DataRow("table", new[] { "column1" }, "SELECT column1 FROM table")]
    public void SelectQueryWithoutConditions(string tableName, string[] columns, string expected)
    {
        var builder = new SelectQueryBuilder();
        var actual = builder.From(tableName).Columns(columns.ToList()).Build();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("table", new[] { "column1", "column2" }, "column1 = value1",
             "SELECT column1, column2 FROM table")]
    public void SelectQueryWithNullConditions(string tableName, string[] columns, string condition, string expected)
    {
        var builder = new SelectQueryBuilder();
        var actual = builder.From(tableName).Columns(columns.ToList()).Where(null).Build();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("table", new[] { "column1", "column2" }, "SELECT ALL FROM table")]
    public void SelectQueryWithNullColumns(string tableName, string[] columns, string expected)
    {
        var builder = new SelectQueryBuilder();
        var actual = builder.From(tableName).Columns(null).Build();
        Assert.AreEqual(expected, actual);
    }
}

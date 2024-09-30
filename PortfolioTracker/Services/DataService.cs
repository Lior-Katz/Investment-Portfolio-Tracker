using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using PortfolioTracker.Models;
using PortfolioTracker.Utils.QueryBuilder;
using PortfolioTracker.ViewModels;
using SqlCommandBuilder = PortfolioTracker.Utils.QueryBuilder.SqlCommandBuilder;

namespace PortfolioTracker.Services;

/// <summary>
///     This class provides data services for interacting with a portfolio tracking system.
/// </summary>
public static class DataService
{
    private static string ConnectionString { get; } =
        ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;

    public static bool isPortfoliosEmpty()
    {
        var checkEntriesQuery = "SELECT COUNT(*) FROM Portfolios";

        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

        using var command = new SqlCommand(checkEntriesQuery, connection);
        return (int)command.ExecuteScalar() == 0;
    }

    /// <summary>
    ///     Retrieves a portfolio from the database based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the portfolio to retrieve.</param>
    /// <returns>A  <see cref="PortfolioViewModel" /> representing the retrieved portfolio.</returns>
    public static PortfolioViewModel InitPortfolio(int id)
    {
        var portfolio = RetrievePortfolio(id);

        // Retrieve associated transactions and holdings
        portfolio.Trades = RetrieveTransactions(portfolio.Id);
        portfolio.Holdings = RetrieveHoldings(portfolio.Id);

        // Retrieve historical value
        var portfolioHistoricalValue = retrieveHistoricalValues(portfolio.Id).ToList();
        portfolioHistoricalValue.Sort();
        portfolio.HistoricalValue = portfolioHistoricalValue;

        return portfolio;
    }

    private static PortfolioViewModel RetrievePortfolio(int id)
    {
        PortfolioViewModel portfolio;
        using var command = new SqlCommandBuilder().Connection(new SqlConnection(ConnectionString))
                                                   .Select(new List<string> { "name", "CreationDate" })
                                                   .From("Portfolios")
                                                   .Where(new SelectQueryBuilder.SearchPredicate("id",
                                                                   QueryOperator.EQUALS, "@id"))
                                                   .BuildCommand();
        command.Parameters.AddWithValue("@id", id);


        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            // Portfolio exists in the database
            portfolio = new PortfolioViewModel(new Portfolio(reader.GetString(reader.GetOrdinal("name")),
                                                             reader.GetDateTime(reader.GetOrdinal("CreationDate"))));
            portfolio.Id = id;
        }
        else
        {
            // Portfolio does not exist, create a new one with default values
            portfolio = new PortfolioViewModel(new Portfolio("Untitled Portfolio", DateTime.Today));
            portfolio.Id = WriteToSQL(portfolio);
        }

        return portfolio;
    }

    public static DateTime? GetLastSavedHistoryDate(int portfolioId)
    {
        using SqlCommand command = new SqlCommandBuilder().Connection(new SqlConnection(ConnectionString))
                                                          .Select(new List<string> { "MAX(date)" })
                                                          .From("ValueHistory")
                                                          .Where(new SelectQueryBuilder.SearchPredicate("portfolioId", QueryOperator.EQUALS, "@portfolioId"))
                                                          .BuildCommand();
        
        command.Parameters.AddWithValue("@portfolioId", portfolioId);
        
        using var reader = command.ExecuteReader();
        
        return reader.Read() && !reader.IsDBNull(0) ? reader.GetDateTime(reader.GetOrdinal("MAX(date)")) : null;
    }

    /// <summary>
    ///     Writes a portfolio to the database and returns the generated ID.
    /// </summary>
    /// <param name="portfolio">The PortfolioViewModel to write to the database.</param>
    /// <returns>The generated ID of the written portfolio.</returns>
    public static int WriteToSQL(PortfolioViewModel portfolio)
    {
        // using SqlConnection connection = new SqlConnection(ConnectionString);
        //
        // connection.Open();
        //
        // string query = "INSERT INTO Portfolios (name, CreatedDate) OUTPUT INSERTED.id VALUES (@Name, @CreatedDate)";
        //
        // using SqlCommand command = CreateCommand(query, connection, new Dictionary<string, object>
        // {
        // 	["@Name"] = portfolio.Name,
        // 	["@CreatedDate"] = portfolio.createdDate
        // });

        using var command = new SqlCommandBuilder().Connection(new SqlConnection(ConnectionString))
                                                   .Insert(new Dictionary<string, object>
                                                           {
                                                               ["name"] = portfolio.Name,
                                                               ["CreatedDate"] = portfolio.createdDate
                                                           })
                                                   .Into("Portfolios")
                                                   .Output(new List<string> { "INSERTED.id" })
                                                   .BuildCommand();

        // Return the generated id
        return (int)command.ExecuteScalar();
    }

    /// <summary>
    ///     Writes a holding to the database and returns the generated ID.
    /// </summary>
    /// <param name="portfolioId">The ID of the portfolio associated with the holding.</param>
    /// <param name="holding">The Holding object to write to the database.</param>
    /// <returns>The generated ID of the written holding.</returns>
    public static int WriteToSQL(int portfolioId, Holding holding)
    {
        // string query = "INSERT INTO Holdings (portfolioId, name, ticker, quantity, acquisitionDate, type, sector, market) OUTPUT INSERTED.id VALUES (@portfolioId, @name, @ticker, @quantity, @acquisitionDate, @type, @sector, @market)";
        //
        // using SqlConnection connection = new SqlConnection(ConnectionString);
        // connection.Open();
        //
        // using SqlCommand command = CreateCommand(query, connection, new Dictionary<string, object>
        // {
        // 	["@portfolioId"] = portfolioId,
        // 	["@name"] = holding.Name,
        // 	["@ticker"] = holding.Ticker,
        // 	["@quantity"] = holding.Quantity,
        // 	["@acquisitionDate"] = holding.AcquisitionDate.ToDateTime(TimeOnly.MinValue),
        // 	["@type"] = holding.Type,
        // 	["@sector"] = holding.Sector,
        // 	["@market"] = holding.Market
        // });

        using var command = new SqlCommandBuilder().Connection(new SqlConnection(ConnectionString))
                                                   .Insert(new Dictionary<string, object>
                                                           {
                                                               ["@portfolioId"] = portfolioId,
                                                               ["@name"] = holding.Name,
                                                               ["@ticker"] = holding.Ticker,
                                                               ["@quantity"] = holding.Quantity,
                                                               ["@acquisitionDate"] =
                                                                   holding.AcquisitionDate
                                                                          .ToDateTime(TimeOnly.MinValue),
                                                               ["@type"] = holding.Type,
                                                               ["@sector"] = holding.Sector,
                                                               ["@market"] = holding.Market
                                                           })
                                                   .Into("Holdings")
                                                   .Output(new List<string> { "INSERTED.id" })
                                                   .BuildCommand();

        // Return the generated id
        return (int)command.ExecuteScalar();
    }

    /// <summary>
    ///     Writes a trade to the database and returns the generated ID.
    /// </summary>
    /// <param name="portfolioId">The ID of the portfolio associated with the trade.</param>
    /// <param name="trade">The Trade object to write to the database.</param>
    /// <returns>The generated ID of the written trade.</returns>
    public static int WriteToSQL(int portfolioId, Trade trade)
    {
        // string query = "INSERT INTO Transactions (portfolioId, date, name, ticker, quantity, price, tax, commission, orderType, currency) OUTPUT INSERTED.id VALUES (@portfolioId, @date, @name, @ticker, @quantity, @price, @tax, @commission, @orderType, @currency)";
        //
        // using SqlConnection connection = new SqlConnection(ConnectionString);
        // connection.Open();
        // using SqlCommand command = CreateCommand(query, connection, new Dictionary<string, object>
        // {
        // 	["@portfolioId"] = portfolioId,
        // 	["@date"] = trade.Date.ToDateTime(TimeOnly.MinValue),
        // 	["@name"] = trade.Name,
        // 	["@ticker"] = trade.Ticker,
        // 	["@quantity"] = trade.Quantity,
        // 	["@price"] = trade.Price,
        // 	["@tax"] = trade.Tax,
        // 	["@commission"] = trade.Commission,
        // 	["@orderType"] = trade.IsBuyOrder ? "Buy" : "Sell",
        // 	["@currency"] = trade.Currency.ToString()
        // });

        using var command = new SqlCommandBuilder().Connection(new SqlConnection(ConnectionString))
                                                   .Insert(new Dictionary<string, object>
                                                           {
                                                               ["portfolioId"] = portfolioId,
                                                               ["date"] =  trade.Date.ToDateTime(TimeOnly.MinValue).ToString("yyyy-MM-dd HH:mm:ss"),
                                                               ["name"] = trade.Name,
                                                               ["ticker"] = trade.Ticker,
                                                               ["quantity"] = trade.Quantity,
                                                               ["price"] = trade.Price,
                                                               ["tax"] = trade.Tax,
                                                               ["commission"] = trade.Commission,
                                                               ["orderType"] = trade.IsBuyOrder ? "Buy" : "Sell",
                                                               ["currency"] = trade.Currency.ToString()
                                                           })
                                                   .Into("Transactions")
                                                   .Output(new List<string> { "INSERTED.id" })
                                                   .BuildCommand();

        // Return the generated id
        return (int)command.ExecuteScalar();
    }

    public static void WriteToSQL(int portfolioId, List<KeyValuePair<DateTime, decimal>> historicalValues)
    {
        // string query = "INSERT INTO ValueHistory (portfolioId, date, value) VALUES (@portfolioId, @date, @value)";
        // using SqlConnection connection = new SqlConnection(ConnectionString);
        // connection.Open();
        // using SqlCommand command = CreateCommand(query, connection, new Dictionary<string, object>
        // 	                                                    {
        // 		                                                    ["@portfolioId"] = portfolioId,
        // 		                                                    ["@date"] = null,
        // 		                                                    ["@value"] = null
        // 	                                                    });

        var cmd = new SqlCommandBuilder().Connection(new SqlConnection(ConnectionString))
                                         .Insert(new Dictionary<string, object>
                                                 {
                                                     ["portfolioId"] = portfolioId,
                                                     ["date"] = null,
                                                     ["value"] = null
                                                 })
                                         .Into("ValueHistory")
                                         .BuildCommand();
    }


    /// <summary>
    ///     Retrieves holdings from the database associated with a specific portfolio.
    /// </summary>
    /// <param name="portfolioId">The ID of the portfolio.</param>
    /// <returns>An ObservableCollection of  <see cref="HoldingViewModel" /> representing the retrieved holdings.</returns>
    private static ObservableCollection<HoldingViewModel> RetrieveHoldings(int portfolioId)
    {
        var holdings = new ObservableCollection<HoldingViewModel>();
        // string retrieveQuery = "SELECT id, name, ticker, quantity, acquisitionDate, type, sector, market, payoutYield, payoutTax, payoutCommission, payoutPeriod, payoutLastPaid FROM Holdings WHERE portfolioId = @portfolioId";
        //
        // using SqlConnection connection = new SqlConnection(ConnectionString);
        // connection.Open();
        //
        // using SqlCommand command = new SqlCommand(retrieveQuery, connection);

        using var command = new SqlCommandBuilder().Connection(new SqlConnection(ConnectionString))
                                                   .Select(new List<string>
                                                           {
                                                               "id", "name", "ticker", "quantity", "acquisitionDate",
                                                               "type", "sector", "market", "payoutYield", "payoutTax",
                                                               "payoutCommission", "payoutPeriod", "payoutLastPaid"
                                                           })
                                                   .From("Holdings")
                                                   .Where(new SelectQueryBuilder.SearchPredicate("portfolioId",
                                                                   QueryOperator.EQUALS, "@portfolioId"))
                                                   .BuildCommand();
        command.Parameters.AddWithValue("@portfolioId", portfolioId);

        using var reader = command.ExecuteReader();

        while (reader.Read()) holdings.Add(CreateHoldingViewModelFromSQLReader(reader));
        return holdings;
    }

    /// <summary>
    ///     Retrieves transactions from the database associated with a specific portfolio.
    /// </summary>
    /// <param name="portfolioId">The ID of the portfolio.</param>
    /// <returns>An ObservableCollection of  <see cref="TradeViewModel" /> representing the retrieved transactions.</returns>
    private static ObservableCollection<TradeViewModel> RetrieveTransactions(int portfolioId)
    {
        var trades = new ObservableCollection<TradeViewModel>();
        foreach (var trade in getTradesWithCondition(
                                                     "portfolioId = @portfolioId", ("@portfolioId", portfolioId)))
            trades.Add(new TradeViewModel(trade));
        return trades;
    }

    private static IEnumerable<KeyValuePair<DateTime, decimal>> retrieveHistoricalValues(int portfolioId)
    {
        // string retrieveQuery = "SELECT date, value FROM ValueHistory WHERE portfolioId = @portfolioId";
        //
        // using SqlConnection connection = new SqlConnection(ConnectionString);
        // connection.Open();
        // using SqlCommand command = new SqlCommand(retrieveQuery, connection);

        using var command = new SqlCommandBuilder().Connection(new SqlConnection(ConnectionString))
                                                   .Select(new List<string> { "date", "value" })
                                                   .From("ValueHistory")
                                                   .Where(new SelectQueryBuilder.SearchPredicate("portfolioId",
                                                                   QueryOperator.EQUALS, "@portfolioId"))
                                                   .BuildCommand();

        command.Parameters.AddWithValue("@portfolioId", portfolioId);

        using var reader = command.ExecuteReader();

        while (reader.Read())
            yield return new KeyValuePair<DateTime, decimal>(reader.GetDateTime(reader.GetOrdinal("date")),
                                                             reader.GetDecimal(reader.GetOrdinal("value")));
    }

    /// <summary>
    ///     Writes a payout for a holding to the database and returns the generated ID.
    /// </summary>
    /// <param name="holding">The Holding object for which to write a payout.</param>
    /// <param name="date">The date of the payout.</param>
    /// <returns>The generated ID of the written payout.</returns>
    public static int WriteToSQL(Holding holding, DateOnly date)
    {
        // string writePayoutQuery = "INSERT INTO Payouts (holdingId, amount, date, tax, commission) OUTPUT INSERTED.id VALUES (@holdingId, @amount, @date, @tax, @commission)";
        //
        // using SqlConnection connection = new SqlConnection(ConnectionString);
        // connection.Open();
        // using SqlCommand command = CreateCommand(writePayoutQuery, connection, new Dictionary<string, object>
        // {
        //
        // 	["@holdingId"] = holding.Id,
        // 	["@amount"] = holding.Value * holding.Payout?.Yield,
        // 	["@date"] = date,
        // 	["@tax"] = holding.Payout?.Tax,
        // 	["@commission"] = holding.Payout?.Commission
        // });

        using var command = new SqlCommandBuilder().Connection(new SqlConnection(ConnectionString))
                                                   .Insert(new Dictionary<string, object>
                                                           {
                                                               ["holdingId"] = holding.Id,
                                                               ["amount"] = holding.Value * holding.Payout?.Yield,
                                                               ["date"] = date,
                                                               ["tax"] = holding.Payout?.Tax,
                                                               ["commission"] = holding.Payout?.Commission
                                                           })
                                                   .Into("Payouts")
                                                   .Output(new List<string> { "INSERTED.id" })
                                                   .BuildCommand();

        return (int)command.ExecuteScalar();
    }

    /// <summary>
    ///     Retrieves trades for a holding based on its ticker from the database.
    /// </summary>
    /// <param name="ticker">The ticker symbol of the holding.</param>
    /// <returns>A list of  <see cref="Trade" /> objects associated with the specified holding.</returns>
    private static List<Trade> getHoldingTrades(string ticker)
    {
        return getTradesWithCondition(
                                      "ticker = @ticker", ("@ticker", ticker)).ToList();
    }

    /// <summary>
    ///     Retrieves the trades the satisfy <paramref name="condition" /> from database.
    /// </summary>
    /// <param name="condition">A string representing the condition to impose on the retrieved trades.</param>
    /// <param name="queryParams">
    ///     An array of (string paramName, object value), that represent the value to be passed into
    ///     paramName placeholder in the condition.
    /// </param>
    /// <returns>An IEnumerable of <see cref="Trade" /> objects that represent the trades retrieved.</returns>
    /// <remarks>
    ///     <paramref name="condition" /> must be an SQL Server conditional statement, without WHERE keyword, and names of
    ///     parameters must be "@paramName".
    ///     paramName of <paramref name="queryParams" /> must be a string "@paramName". value is the value to use in place of
    ///     @paramName.
    /// </remarks>
    /// <example>
    ///     getTradesWithCondition("myFirstColumn = @myNum OR mySecondColumn = @myStr", [("@myNum", 5), ("@myStr", "hello"))
    /// </example>
    private static IEnumerable<Trade> getTradesWithCondition(string condition,
                                                             params (string paramName, object value)[] queryParams)
    {
        var getTransactionsQuery =
            "SELECT id, date, name, ticker, quantity, price, tax, commission, orderType, currency FROM Transactions WHERE " +
            condition;

        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        using var command = new SqlCommand(getTransactionsQuery, connection);

        foreach (var tup in queryParams) command.Parameters.AddWithValue(tup.paramName, tup.value);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var id = reader.GetInt32(reader.GetOrdinal("id"));
            var date = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("date")));
            var name = reader.GetString(reader.GetOrdinal("name"));
            var ticker = reader.GetString(reader.GetOrdinal("ticker"));
            var quantity = reader.GetDecimal(reader.GetOrdinal("quantity"));
            var price = reader.GetDecimal(reader.GetOrdinal("price"));
            var tax = reader.IsDBNull(reader.GetOrdinal("tax")) ? 0 : reader.GetDecimal(reader.GetOrdinal("tax"));
            var commission = reader.IsDBNull(reader.GetOrdinal("commission"))
                                 ? 0
                                 : reader.GetDecimal(reader.GetOrdinal("commission"));
            var isBuyOrder = reader.GetString(reader.GetOrdinal("orderType")) == "Sell" ? true : false;
            var currency = reader.GetString(reader.GetOrdinal("currency"));

            yield return new Trade(id, name, ticker, isBuyOrder, date, quantity, price, tax, commission,
                                   new CurrencyModel(currency));
        }
    }

    /// <summary>
    ///     Creates a HoldingViewModel from the data retrieved from the SQL database.
    /// </summary>
    /// <param name="reader">The SqlDataReader containing the holding data.</param>
    /// <returns>A  <see cref="HoldingViewModel" /> representing the retrieved holding.</returns>
    private static HoldingViewModel CreateHoldingViewModelFromSQLReader(SqlDataReader reader)
    {
        var id = reader.GetInt32(reader.GetOrdinal("id"));
        var name = reader.GetString(reader.GetOrdinal("name"));
        var ticker = reader.GetString(reader.GetOrdinal("ticker"));
        var quantity = reader.GetDecimal(reader.GetOrdinal("quantity"));
        var acquisitionDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("acquisitionDate")));
        var type = reader.IsDBNull(reader.GetOrdinal("type")) ? "" : reader.GetString(reader.GetOrdinal("type"));
        var sector = reader.IsDBNull(reader.GetOrdinal("sector")) ? "" : reader.GetString(reader.GetOrdinal("sector"));
        var market = reader.IsDBNull(reader.GetOrdinal("market")) ? "" : reader.GetString(reader.GetOrdinal("market"));
        var payoutYield = reader.IsDBNull(reader.GetOrdinal("payoutYield"))
                              ? 0
                              : reader.GetDecimal(reader.GetOrdinal("payoutYield"));
        var payoutTax = reader.IsDBNull(reader.GetOrdinal("payoutTax"))
                            ? 0
                            : reader.GetDecimal(reader.GetOrdinal("payoutTax"));
        var payoutCommission = reader.IsDBNull(reader.GetOrdinal("payoutCommission"))
                                   ? 0
                                   : reader.GetDecimal(reader.GetOrdinal("payoutCommission"));
        var payoutPeriod = reader.IsDBNull(reader.GetOrdinal("payoutPeriod"))
                               ? 0
                               : reader.GetInt32(reader.GetOrdinal("payoutPeriod"));
        DateOnly? payoutLastPaid = reader.IsDBNull(reader.GetOrdinal("payoutLastPaid"))
                                       ? null
                                       : DateOnly
                                           .FromDateTime(reader.GetDateTime(reader.GetOrdinal("acquisitionDate")));

        return new HoldingViewModel(new Holding(name, ticker, quantity, acquisitionDate, payoutYield, payoutTax,
                                                payoutCommission, payoutPeriod, type, sector, market, payoutLastPaid,
                                                getHoldingTrades(ticker), id));
    }

    /// <summary>
    ///     Creates a SqlCommand with parameters from a dictionary.
    /// </summary>
    /// <param name="query">The SQL query string.</param>
    /// <param name="connection">The SqlConnection to use.</param>
    /// <param name="dictionary">A dictionary of parameters and their values.</param>
    /// <returns>A  <see cref="SqlCommand" /> with the specified parameters.</returns>
    private static SqlCommand CreateCommand(string query, SqlConnection connection,
                                            Dictionary<string, object> dictionary)
    {
        var command = new SqlCommand(query, connection);

        foreach (var (parameterName, value) in dictionary) command.Parameters.AddWithValue(parameterName, value);

        return command;
    }
}

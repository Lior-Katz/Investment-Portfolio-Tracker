using PortfolioTracker.Models;
using PortfolioTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PortfolioTracker.Services
{
	public static class DataService
	{
		private static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;


		public static bool isPortfoliosEmpty()
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				string checkEntriesQuery = "SELECT COUNT(*) FROM Portfolios";

				using (SqlCommand command = new SqlCommand(checkEntriesQuery, connection))
				{
					return (int) command.ExecuteScalar() == 0;
				}
			}
		}

		public static PortfolioViewModel RetrievePortfolio(int id)
		{
			PortfolioViewModel portfolio;

			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				string retrieveQuery = "SELECT name FROM Portfolios WHERE id = @id";
				connection.Open();

				using (SqlCommand command = new SqlCommand(retrieveQuery, connection))
				{
					command.Parameters.AddWithValue("@id", id);

					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							portfolio = new PortfolioViewModel(new Portfolio(reader.GetString(reader.GetOrdinal("name"))));
							portfolio.Id = id;
						}
						else
						{
							portfolio = new PortfolioViewModel(new Portfolio("Untitled Portfolio"));
							portfolio.Id = WriteData(portfolio);
						}
					}
				}
			}

			portfolio.Trades = RetrieveTransactions(portfolio.Id);
			portfolio.Holdings = RetrieveHoldings(portfolio.Id);
			return portfolio;
		}



		public static int WriteData(PortfolioViewModel portfolio)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				string query = "INSERT INTO Portfolios (name) OUTPUT INSERTED.id VALUES (@Name)";

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Name", portfolio.Name);

					// return generated id
					return (int) command.ExecuteScalar();
				}

			}
		}

		public static int WriteData(int portfolioId, Holding holding)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				string query = "INSERT INTO Holdings (portfolioId, name, ticker, quantity, acquisitionDate, type, sector, market) OUTPUT INSERTED.id VALUES (@portfolioId, @name, @ticker, @quantity, @acquisitionDate, @type, @sector, @market)";

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@portfolioId", portfolioId);
					command.Parameters.AddWithValue("@name", holding.Name);
					command.Parameters.AddWithValue("@ticker", holding.Ticker);
					command.Parameters.AddWithValue("@quantity", holding.Quantity);
					command.Parameters.AddWithValue("@acquisitionDate", holding.AcquisitionDate.ToDateTime(TimeOnly.MinValue));
					command.Parameters.AddWithValue("@type", holding.Type);
					command.Parameters.AddWithValue("@sector", holding.Sector);
					command.Parameters.AddWithValue("@market", holding.Market);

					// return generated id
					return (int) command.ExecuteScalar();
				}

			}
		}

		public static int WriteData(int portfolioId, Trade trade)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				string query = "INSERT INTO Transactions (portfolioId, date, name, ticker, quantity, price, tax, commission, orderType, currency) OUTPUT INSERTED.id VALUES (@portfolioId, @date, @name, @ticker, @quantity, @price, @tax, @commission, @orderType, @currency)";

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@portfolioId", portfolioId);
					command.Parameters.AddWithValue("@date", trade.Date.ToDateTime(TimeOnly.MinValue));
					command.Parameters.AddWithValue("@name", trade.Name);
					command.Parameters.AddWithValue("@ticker", trade.Ticker);
					command.Parameters.AddWithValue("@quantity", trade.Quantity);
					command.Parameters.AddWithValue("@price", trade.Price);
					command.Parameters.AddWithValue("@tax", trade.Tax);
					command.Parameters.AddWithValue("@commission", trade.Commission);
					command.Parameters.AddWithValue("@orderType", trade.IsBuyOrder ? "Buy" : "Sell");
					command.Parameters.AddWithValue("@currency", trade.Currency.ToString());


					// return generated id
					return (int) command.ExecuteScalar();
				}

			}
		}

		private static ObservableCollection<HoldingViewModel> RetrieveHoldings(int portfolioId)
		{
			ObservableCollection<HoldingViewModel> holdings = new ObservableCollection<HoldingViewModel>();
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				string retrieveQuery = "SELECT id, name, ticker, quantity, acquisitionDate, type, sector, market, payoutYield, payoutTax, payoutCommission, payoutPeriod, payoutLastPaid FROM Holdings WHERE portfolioId = @portfolioId";

				connection.Open();

				using (SqlCommand command = new SqlCommand(retrieveQuery, connection))
				{
					command.Parameters.AddWithValue("@portfolioId", portfolioId);

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							holdings.Add(createHoldingViewModel(reader));
						}
					}
				}
			}
			return holdings;
		}

		private static ObservableCollection<TradeViewModel> RetrieveTransactions(int portfolioId)
		{
			ObservableCollection<TradeViewModel> trades = new ObservableCollection<TradeViewModel>();
			foreach (Trade trade in getTradesWithCondition(
				"portfolioId = @portfolioId",
				new (string, object)[] { ("@portfolioId", portfolioId) }))
			{
				trades.Add(new TradeViewModel(trade));
			}
			return trades;
		}

		public static int WritePayout(Holding holding, DateOnly date)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				string writePayoutQuery = "INSERT INTO Payouts (holdingId, amount, date, tax, commission) OUTPUT INSERTED.id VALUES (@holdingId, @amount, @date, @tax, @commission)";

				using (SqlCommand command = new SqlCommand(writePayoutQuery, connection))
				{
					command.Parameters.AddWithValue("@holdingId", holding.Id);
					command.Parameters.AddWithValue("@amount", holding.Value * holding.Payout?.Yield);
					command.Parameters.AddWithValue("@date", date);
					command.Parameters.AddWithValue("@tax", holding.Payout?.Tax);
					command.Parameters.AddWithValue("@commission", holding.Payout?.Commission);

					return (int) command.ExecuteScalar();
				}
			}
		}

		private static List<Trade> getHoldingTrades(string ticker)
		{
			return getTradesWithCondition(
				 "ticker = @ticker",
				  new (string, object)[] { ("@ticker", ticker) }).ToList();
		}

		/// <summary>
		/// Retrieves the trades the satisfy <paramref name="condition"/> from database.
		/// </summary>
		/// <param name="condition">A string representing the condition to impose on the retrieved trades.</param>
		/// <param name="queryParams">An array of (string paramName, object value), that represent the value to be passed into paramName placeholder in the condition.</param>
		/// <returns>An iterator of <see cref="Trade" /> objects that represent the trades retrieved.</returns>
		/// <remarks>
		/// <paramref name="condition"/> must be an SQL Server conditional statement, without WHERE keyword, and names of parameters must be "@paramName".
		/// paramName of <paramref name="queryParams"/> must be a string "@paramName". value is the value to use in place of @paramName.
		/// </remarks>
		/// <example>
		/// getTradesWithCondition("myFirstColumn = @myNum OR mySecondColumn = @myStr", [("@myNum", 5), ("@myStr", "hello"))
		/// </example>
		private static IEnumerable<Trade> getTradesWithCondition(string condition, params (string paramName, object value)[] queryParams)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				string getTransactionsQuery = "SELECT id, date, name, ticker, quantity, price, tax, commission, orderType, currency FROM Transactions WHERE " + condition;

				using (SqlCommand command = new SqlCommand(getTransactionsQuery, connection))
				{
					foreach ((string paramName, object value) tup in queryParams)
					{
						command.Parameters.AddWithValue(tup.paramName, tup.value);
					}
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							int id = reader.GetInt32(reader.GetOrdinal("id"));
							DateOnly date = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("date")));
							string name = reader.GetString(reader.GetOrdinal("name"));
							string ticker = reader.GetString(reader.GetOrdinal("ticker"));
							decimal quantity = reader.GetDecimal(reader.GetOrdinal("quantity"));
							decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
							decimal tax = reader.IsDBNull(reader.GetOrdinal("tax")) ? 0 : reader.GetDecimal(reader.GetOrdinal("tax"));
							decimal commission = reader.IsDBNull(reader.GetOrdinal("commission")) ? 0 : reader.GetDecimal(reader.GetOrdinal("commission"));
							bool isBuyOrder = reader.GetString(reader.GetOrdinal("orderType")) == "Sell" ? true : false;
							string currency = reader.GetString(reader.GetOrdinal("currency"));

							yield return new Trade(id, name, ticker, isBuyOrder, date, quantity, price, tax, commission, new CurrencyModel(currency));
						}
					}
				}
			}
		}
		private static HoldingViewModel createHoldingViewModel(SqlDataReader reader)
		{
			int id = reader.GetInt32(reader.GetOrdinal("id"));
			string name = reader.GetString(reader.GetOrdinal("name"));
			string ticker = reader.GetString(reader.GetOrdinal("ticker"));
			decimal quantity = reader.GetDecimal(reader.GetOrdinal("quantity"));
			DateOnly acquisitionDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("acquisitionDate")));
			string type = reader.IsDBNull(reader.GetOrdinal("type")) ? "" : reader.GetString(reader.GetOrdinal("type"));
			string sector = reader.IsDBNull(reader.GetOrdinal("sector")) ? "" : reader.GetString(reader.GetOrdinal("sector"));
			string market = reader.IsDBNull(reader.GetOrdinal("market")) ? "" : reader.GetString(reader.GetOrdinal("market"));
			decimal payoutYield = reader.IsDBNull(reader.GetOrdinal("payoutYield")) ? 0 : reader.GetDecimal(reader.GetOrdinal("payoutYield"));
			decimal payoutTax = reader.IsDBNull(reader.GetOrdinal("payoutTax")) ? 0 : reader.GetDecimal(reader.GetOrdinal("payoutTax"));
			decimal payoutCommission = reader.IsDBNull(reader.GetOrdinal("payoutCommission")) ? 0 : reader.GetDecimal(reader.GetOrdinal("payoutCommission"));
			int payoutPeriod = reader.IsDBNull(reader.GetOrdinal("payoutPeriod")) ? 0 : reader.GetInt32(reader.GetOrdinal("payoutPeriod"));
			DateOnly? payoutLastPaid = reader.IsDBNull(reader.GetOrdinal("payoutLastPaid")) ? null : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("acquisitionDate")));

			return new HoldingViewModel(new Holding(name, ticker, quantity, acquisitionDate, payoutYield, payoutTax, payoutCommission, payoutPeriod, type, sector, market, payoutLastPaid, getHoldingTrades(ticker), id));
		}
	}
}

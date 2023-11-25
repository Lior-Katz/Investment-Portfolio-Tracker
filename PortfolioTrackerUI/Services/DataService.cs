using PortfolioTracker.Models;
using PortfolioTracker.ViewModels;
using System;
using System.Configuration;
using System.Data.SqlClient;

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

		public static Portfolio GetData()
		{
			throw new NotImplementedException();
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
					command.Parameters.AddWithValue("@orderType", "Sell" /*trade.OrderType*/);
					command.Parameters.AddWithValue("@currency", trade.Currency.ToString());


					// return generated id
					return (int) command.ExecuteScalar();
				}

			}
		}
	}
}

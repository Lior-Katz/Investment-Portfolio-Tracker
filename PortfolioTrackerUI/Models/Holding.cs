using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioTracker.Models
{
	public class Holding : IEquatable<Holding>
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int Id { get; private set; }
		/// <summary>
		/// Asset Name
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// The 3- or 4-letter symbol of the asset
		/// </summary>
		public string Ticker { get; set; }
		/// <summary>
		/// Number of shares held.
		/// </summary>
		public decimal Quantity { get; set; }
		/// <summary>
		/// List of all trades of this asset.
		/// </summary>
		public List<Trade> Trades { get; set; } = new List<Trade>();
		/// <summary>
		/// Initial date when this asset was aquired.
		/// </summary>
		public DateOnly AquisitionDate { get; set; }
		/// <summary>
		/// The periodical payout of this asset.
		/// </summary>
		public Payout Payout { get; set; }
		/// <summary>
		/// The type of investment vehicle.
		/// </summary>
		/// <example>
		/// Company stock, index fund, bond, etc.
		/// </example>
		public string Type { get; set; }
		/// <summary>
		/// The sector of this asset.
		/// </summary>
		public string Sector { get; set; }
		/// <summary>
		/// The market where this asset is traded.
		/// </summary>
		public string Market { get; set; }

		public decimal AveragePrice => getAveragePrice(Trades, Quantity);

		public decimal CurrentPrice => getCurrentPrice(Ticker);

		public decimal Value => CurrentPrice * Quantity;

		public decimal DailyChangePercentage => (DailyChange / Value) * 100;

		public decimal DailyChange => getDailyChange(Ticker, Quantity, Value);

		public decimal PercentofPortfolio => getPercentOfPortfolio(Id);


		/// <summary>
		/// Initializes a new instance of the Holding class that has an empty Trades list.
		/// </summary>
		/// <param name="name">Asset name </param>
		/// <param name="ticker">Ticker symbol</param>
		/// <param name="quantity">Quantity held</param>
		/// <param name="aquisitionDate">Initial date of aquisition</param>
		/// <param name="payout">The periodic payout</param>
		/// <param name="type">Type of investment vehicle</param>
		/// <param name="secotr">Sector</param>
		/// <param name="market">Market</param>
		public Holding(string name, string ticker, decimal quantity, DateOnly aquisitionDate, decimal yield, decimal payoutTax, decimal payoutCommission, int payoutPeriodInMonths, string type, string secotr, string market)
		{
			this.Name = name;
			this.Ticker = ticker;
			this.Quantity = quantity;
			this.AquisitionDate = aquisitionDate;
			this.Payout = new Payout(yield, payoutTax, payoutCommission, payoutPeriodInMonths, aquisitionDate);
			this.Type = type;
			this.Sector = secotr;
			this.Market = market;
		}

		private static decimal getAveragePrice(List<Trade> trades, decimal quantity)
		{
			decimal totalSpent = 0, totalMade = 0;
			foreach (Trade trade in trades)
			{
				decimal value = trade.Quantity * trade.Price;
				value += value * trade.Commission + value * trade.Tax;
				if (trade.IsBuyOrder)
					totalSpent += value;
				else
					totalMade += value;
			}

			return (totalSpent - totalMade) / quantity;
		}

		// TODO: implement
		private decimal getCurrentPrice(string ticker) => throw new NotImplementedException();

		// TODO: implement
		private decimal getDailyChange(string ticker, decimal quantity, decimal value) => throw new NotImplementedException();

		// TODO: implement
		private decimal getPercentOfPortfolio(int id) => throw new NotImplementedException();


		public void addTrade(Trade trade)
		{
			if (trade == null)
				throw new NullReferenceException("null trade added");

			if (Trades.Contains(trade))
				return;

			Trades.Add(trade);
		}

		public void removeTrade(Trade trade)
		{
			Trades.Remove(trade);
		}

		public override bool Equals(object? other)
		{
			return other is Holding holding && this.Id == holding.Id;
		}

		public bool Equals(Holding? other)
		{
			return other is not null && this.Id == other.Id;
		}

		public static bool operator ==(Holding? lvalue, Holding? rvalue)
		{
			return (lvalue is null && rvalue is null) ||
				(lvalue is not null && lvalue.Equals(rvalue));
		}

		public static bool operator !=(Holding? lvalue, Holding? rvalue)
		{
			return !(lvalue == rvalue);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}

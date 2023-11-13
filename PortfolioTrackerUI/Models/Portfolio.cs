using System;
using System.Collections.Generic;

namespace PortfolioTracker.Models
{
	public class Portfolio
	{
		/// <summary>
		/// The unique identifier of the portfolio.
		/// </summary>
		public int Id { get; private set; }
		/// <summary>
		/// The name of the portfolio.
		/// </summary>
		public string Name { get; set; } = string.Empty;
		/// <summary>
		/// A list of all assets currently held in the portfolio.
		/// </summary>
		public List<Holding> Holdings { get; } = new List<Holding>();
		/// <summary>
		/// A record of all trades recorded in this portfolio.
		/// </summary>
		public List<Trade> Trades { get; } = new List<Trade>();

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Portfolio Name</param>
		public Portfolio(string name)
		{
			this.Name = name;
			Holdings = new List<Holding>();
			Trades = new List<Trade>();
		}

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

		public void addHolding(Holding holding)
		{
			if (holding == null)
				throw new NullReferenceException();

			if (Holdings.Contains(holding))
				return;

			Holdings.Add(holding);
		}

		public void removeHolding(Holding holding)
		{
			Holdings.Remove(holding);
		}




		// TODO: allow tracking of historical portfolio performance. 
		//public PortfolioHistory PortfolioHistory { get; set; }
	}
}

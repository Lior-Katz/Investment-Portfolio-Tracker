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

		/// <summary>
		/// Adds a transaction to the portfolio's list of transactions, if it doesn't exist already.
		/// If the transaction already exists, nothing is done.
		/// Transactions are distinguished by their ID's.
		/// </summary>
		/// <param name="trade">The transaction to add to the list.</param>
		/// <exception cref="NullReferenceException">Thrown if a null reference is passed as argument.</exception>
		public void addTrade(Trade trade)
		{
			if (trade == null)
				throw new NullReferenceException("null trade added");

			if (Trades.Contains(trade))
				return;

			Trades.Add(trade);
		}

		/// <summary>
		/// Removes a transaction from the holdings's list of transactions, if it exists.
		/// If the transaction doesn't exist, nothing is done.
		/// Transactions are distinguished by their ID's.
		/// </summary>
		/// <param name="trade">The transaction to remove from the list.</param>
		public void removeTrade(Trade trade)
		{
			Trades.Remove(trade);
		}

		/// <summary>
		/// Adds a new asset to the portfolio's list of holdings, if it doesn't exist already.
		/// If the holding already exists, nothing is done.
		/// Holdings are distinguished by their ID's.
		/// </summary>
		/// <param name="holding">The asset to add to the list.</param>
		/// <exception cref="NullReferenceException">Thrown if a null reference is passed as argument.</exception>
		public void addHolding(Holding holding)
		{
			if (holding == null)
				throw new NullReferenceException();

			if (Holdings.Contains(holding))
				return;

			Holdings.Add(holding);
		}

		/// <summary>
		/// Removes a holding from the portfolio's list of holdings, if it exists.
		/// If the holding doesn't exist, nothing is done.
		/// holdings are distinguished by their ID's.
		/// </summary>
		/// <param name="holding">The holding to remove from the list.</param>
		public void removeHolding(Holding holding)
		{
			Holdings.Remove(holding);
		}




		// TODO: allow tracking of historical portfolio performance. 
		//public PortfolioHistory PortfolioHistory { get; set; }
	}
}

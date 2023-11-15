using System;
using System.Collections.Generic;
using System.Linq;

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

		public List<Holding> MostInfluentialHoldings => GetMostInfluentialHoldings();

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
		public void AddHolding(Holding holding)
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

		/// <summary>
		/// Retrieves the top 5 most influential holdings int the portfolio's holdings list based on their daily change percentages.
		/// </summary>
		/// <returns>An List of Holding objects representing the most influential holdings.</returns>
		private List<Holding> GetMostInfluentialHoldings()
		{
			// Initialize a list with the first 5 holdings
			List<Holding> result = new List<Holding>(Holdings.Take(5));

			// Comparison function to sort holdings based on daily change percentage
			Comparison<Holding> comparison = (x, y) => x.DailyChangePercentage.CompareTo(y.DailyChangePercentage);

			// Sort the initial list of holdings
			result.Sort(comparison);

			// Iterate over remaining holdings to find the most influential ones
			foreach (Holding portfolioHolding in Holdings.Skip(result.Count))
			{
				// Check if the current holding has a higher daily change percentage than the least influential holding in the result
				if (portfolioHolding.DailyChangePercentage > result[result.Count - 1].DailyChangePercentage)
				{
					// Replace the least influential holding with the current holding
					result[result.Count - 1] = portfolioHolding;

					// Re-sort the result to maintain the order
					result.Sort(comparison);
				}
			}

			return result;
		}



		// TODO: allow tracking of historical portfolio performance. 
		//public PortfolioHistory PortfolioHistory { get; set; }
	}
}

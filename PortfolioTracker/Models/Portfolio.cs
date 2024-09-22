using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioTracker.Services;

namespace PortfolioTracker.Models;

public class Portfolio
{
    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="name">Portfolio Name</param>
    /// <param name="createdDate">The date the portfolio was created</param>
    public Portfolio(string name, DateTime createdDate)
    {
        Name = name;
        CreatedDate = createdDate;
    }


    /// <summary>
    ///     The unique identifier of the portfolio.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     The name of the portfolio.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.Today;

    /// <summary>
    ///     A list of all assets currently held in the portfolio.
    /// </summary>
    public List<Holding> Holdings { get; set; } = new();

    /// <summary>
    ///     A record of all trades recorded in this portfolio.
    /// </summary>
    public List<Trade> Trades { get; set; } = new();

    public List<KeyValuePair<DateTime, decimal>> HistoricalValue { get; set; } = new();

    public List<Holding> MostInfluentialHoldings => GetMostInfluentialHoldings();

    public decimal Value
    {
        get
        {
            decimal res = 0;
            foreach (var holding in Holdings) res += holding.Value;
            return Math.Round(res, 2);
        }
    }

    public decimal DailyChange
    {
        get
        {
            decimal res = 0;
            foreach (var holding in Holdings) res += holding.DailyChange;

            return Math.Round(res, 2);
        }
    }

    public decimal DailyChangePercentage
    {
        get
        {
            if (Value == 0)
                return 0;
            return Math.Round(DailyChange / Value * 100, 2);
        }
    }

    /// <summary>
    ///     Adds a transaction to the portfolio's list of transactions, if it doesn't exist already.
    ///     If the transaction already exists, nothing is done.
    ///     Transactions are distinguished by their ID's.
    /// </summary>
    /// <param name="trade">The transaction to add to the list.</param>
    /// <exception cref="NullReferenceException">Thrown if a null reference is passed as argument.</exception>
    public Trade AddTrade(Trade trade)
    {
        if (trade == null)
            throw new NullReferenceException("null trade added");

        //if (Trades.Contains(trade))
        //	throw new ArgumentException("Trade already exists");

        trade.Id = DataService.WriteToSQL(Id, trade);

        Trades.Add(trade);


        // Search for a holding with a matching ticker symbol
        var matchingHolding = Holdings.FirstOrDefault(holding => holding.Ticker == trade.Ticker);
        if (matchingHolding != null)
            // holding with same ticker already exists
            // TODO: this doesn't actually change the holding
            UpdateHoldingWithTrade(ref matchingHolding, trade);

        return trade;
    }


    /// <summary>
    ///     Removes a transaction from the holdings's list of transactions, if it exists.
    ///     If the transaction doesn't exist, nothing is done.
    ///     Transactions are distinguished by their ID's.
    /// </summary>
    /// <param name="trade">The transaction to remove from the list.</param>
    public void RemoveTrade(Trade trade)
    {
        Trades.Remove(trade);
    }

    /// <summary>
    ///     Adds a new asset to the portfolio's list of holdings, if it doesn't exist already.
    ///     If the holding already exists, nothing is done.
    ///     Holdings are distinguished by their ID's.
    /// </summary>
    /// <param name="holding">The asset to add to the list.</param>
    /// <exception cref="NullReferenceException">Thrown if a null reference is passed as argument.</exception>
    public void AddToHoldings(Holding holding)
    {
        if (holding == null)
            throw new NullReferenceException();

        holding.Id = DataService.WriteToSQL(Id, holding);

        Holdings.Add(holding);
    }

    /// <summary>
    ///     Removes a holding from the portfolio's list of holdings, if it exists.
    ///     If the holding doesn't exist, nothing is done.
    ///     holdings are distinguished by their ID's.
    /// </summary>
    /// <param name="holding">The holding to remove from the list.</param>
    public void removeHolding(Holding holding)
    {
        Holdings.Remove(holding);
    }

    /// <summary>
    ///     Retrieves the top 5 most influential holdings int the portfolio's holdings list based on their daily change
    ///     percentages.
    /// </summary>
    /// <returns>An List of Holding objects representing the most influential holdings.</returns>
    private List<Holding> GetMostInfluentialHoldings()
    {
        // Initialize a list with the first 5 holdings
        var result = new List<Holding>(Holdings.Take(5));

        // Comparison function to sort holdings based on daily change percentage
        Comparison<Holding> comparison = (x, y) => x.DailyChangePercentage.CompareTo(y.DailyChangePercentage);

        // Sort the initial list of holdings
        result.Sort(comparison);

        // Iterate over remaining holdings to find the most influential ones
        foreach (var portfolioHolding in Holdings.Skip(result.Count))
            // Check if the current holding has a higher daily change percentage than the least influential holding in the result
            if (portfolioHolding.DailyChangePercentage > result[result.Count - 1].DailyChangePercentage)
            {
                // Replace the least influential holding with the current holding
                result[result.Count - 1] = portfolioHolding;

                // Re-sort the result to maintain the order
                result.Sort(comparison);
            }

        return result;
    }

    private void UpdateHoldingWithTrade(ref Holding matchingHolding, Trade trade)
    {
        matchingHolding.Quantity += trade.Quantity;
        matchingHolding.Trades.Add(trade);
    }

    public decimal GetPercentageOfPortfolio(int holdingId)
    {
        var match = Holdings.Find(holding => holding.Id == holdingId);
        return match != null ? match.Value / Value : 0;
    }

    public bool isHoldingExist(string ticker)
    {
        return Holdings.FirstOrDefault(holding => holding.Ticker == ticker, null) != null;
    }


    // TODO: allow tracking of historical portfolio performance. 
    //public PortfolioHistory PortfolioHistory { get; set; }
}

﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using PortfolioTracker.Services;

namespace PortfolioTracker.Models;

public class Holding : IEquatable<Holding>
{
    /// <summary>
    ///     Initializes a new instance of the Holding class that has an empty Trades list.
    /// </summary>
    /// <param name="name">Asset name </param>
    /// <param name="ticker">Ticker symbol</param>
    /// <param name="quantity">Quantity held</param>
    /// <param name="acquisitionDate">Initial date of acquisition</param>
    /// <param name="payout">The periodic payout</param>
    /// <param name="type">Type of investment vehicle</param>
    /// <param name="sector">Sector</param>
    /// <param name="market">Market</param>
    public Holding(string name, string ticker, decimal quantity, DateOnly acquisitionDate, /*decimal yield,
                   decimal payoutTax, decimal payoutCommission, int payoutPeriodInMonths,*/ string type, string sector,
                   string market, /*DateOnly? payoutLastPaid = null,*/ List<Trade>? trades = null, int id = 0)
    {
        // if (yield > 0 && payoutLastPaid == null)
        //     throw new ArgumentException(nameof(payoutLastPaid) + " is null, while yield > 0");

        Id = id;
        Name = name;
        Ticker = ticker;
        Quantity = quantity;
        AcquisitionDate = acquisitionDate;
        // Payout = yield > 0
        //              ? new Payout(yield, payoutTax, payoutCommission, payoutPeriodInMonths,
        //                           payoutLastPaid != null ? payoutLastPaid.Value : acquisitionDate)
        //              : null;
        Type = type;
        Sector = sector;
        Market = market;

        if (trades != null)
        {
            Trades = trades;
        }
    }

    /// <summary>
    ///     Unique identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Asset Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The 3- or 4-letter symbol of the asset
    /// </summary>
    public string Ticker { get; set; }

    /// <summary>
    ///     Number of shares held.
    /// </summary>
    public decimal Quantity { get; set; }


    /// <summary>
    ///     List of all trades of this asset.
    /// </summary>
    public List<Trade> Trades { get; set; } = new();

    /// <summary>
    ///     Initial date when this asset was aquired.
    /// </summary>
    public DateOnly AcquisitionDate { get; set; }

    /// <summary>
    ///     The periodical payout of this asset.
    /// </summary>
    // public Payout? Payout { get; set; }

    /// <summary>
    ///     The type of investment vehicle.
    /// </summary>
    /// <example>
    ///     Company stock, index fund, bond, etc.
    /// </example>
    public string Type { get; set; }

    /// <summary>
    ///     The sector of this asset.
    /// </summary>
    public string Sector { get; set; }

    /// <summary>
    ///     The market where this asset is traded.
    /// </summary>
    public string Market { get; set; }

    /// <summary>
    ///     Average rate per unit of the currently owned units of this holding.
    ///     Calculated as the differnce between all spending on this holding and all income from selling, all divided by the
    ///     quantity.
    ///     Disregards tax, commissions, and payouts.
    /// </summary>
    public decimal AveragePrice => getAveragePrice(Trades, Quantity);

    private decimal _currentPrice =>
        App.AppHost.Services.GetRequiredService<IFinancialDataService>().GetLastPrice(Ticker);

    /// <summary>
    ///     The current market price of this asset.
    /// </summary>
    public decimal CurrentPrice => Math.Round(_currentPrice, 2);

    private decimal _value => _currentPrice * Quantity;

    /// <summary>
    ///     The total investment value of this asset, based on its current market price.
    ///     Calculated as the product of the current price and the quantity.
    /// </summary>
    public decimal Value => Math.Round(_value, 2);

    /// <summary>
    ///     The daily change in the value of the asset, expressed as a percentage of its current total value.
    /// </summary>
    public decimal DailyChangePercentage
    {
        get
        {
            if (Value == 0)
            {
                return 0;
            }

            return Math.Round(_dailyChange / _value * 100, 2);
        }
    }

    private decimal _dailyChange =>
        App.AppHost.Services.GetRequiredService<IFinancialDataService>().GetDailyChange(Ticker) * Quantity;

    /// <summary>
    ///     The daily change in the value of the asset.
    /// </summary>
    public decimal DailyChange => Math.Round(_dailyChange);

    /// <summary>
    ///     The portion of the the total value of the portfolio that this asset represents, expressed as a percantage.
    /// </summary>
    public decimal PercentOfPortfolio => getPercentOfPortfolio(Id);

    /// <summary>
    ///     Determines whether the current instance is equal to another object.
    /// </summary>
    /// <param name="other">The object to compare with the current instance.</param>
    /// <returns>
    ///     <c>true</c> if the specified object is equal to the current instance;
    ///     otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(Holding? other)
    {
        return other is not null && Id == other.Id;
    }

    /// <summary>
    ///     Calculates the average price of a single share of the asset.
    /// </summary>
    /// <param name="trades">A list of transactions that have been made for this asset</param>
    /// <param name="quantity">The amount of shared currently owned</param>
    /// <returns>The average price per share of the currently owend shares.</returns>
    private static decimal getAveragePrice(List<Trade> trades, decimal quantity)
    {
        decimal totalSpent = 0, totalMade = 0;
        foreach (var trade in trades)
        {
            var value = trade.Quantity * trade.Price;
            value += value * trade.Commission + value * trade.Tax;
            if (trade.IsBuyOrder)
            {
                totalSpent += value;
            }
            else if (!trade.IsBuyOrder)
            {
                totalMade += value;
            }
        }

        return Math.Round((totalSpent - totalMade) / quantity, 2);
    }


    // TODO: implement
    private decimal getPercentOfPortfolio(int id)
    {
        return 1;
    }

    /// <summary>
    ///     Adds a transaction to the holding's list of transactions, if it doesn't exist already.
    ///     If the transaction already exists, nothing is done.
    ///     Transactions are distinguished by their ID's.
    /// </summary>
    /// <param name="trade">The transaction to add to the list.</param>
    /// <exception cref="NullReferenceException">Thrown if a null reference is passed as argument.</exception>
    public void addTrade(Trade trade)
    {
        if (trade == null)
        {
            throw new NullReferenceException("null trade added");
        }

        if (Trades.Contains(trade))
        {
            return;
        }

        Trades.Add(trade);
    }

    /// <summary>
    ///     Removes a transaction from the holdings's list of transactions, if it exists.
    ///     If the transaction doesn't exist, nothing is done.
    ///     Transactions are distinguished by their ID's.
    /// </summary>
    /// <param name="trade">The transaction to remove from the list.</param>
    public void removeTrade(Trade trade)
    {
        Trades.Remove(trade);
    }

    /// <summary>
    ///     Determines whether the current instance is equal to another object.
    /// </summary>
    /// <param name="other">The object to compare with the current instance.</param>
    /// <returns>
    ///     <c>true</c> if the specified object is equal to the current instance;
    ///     otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object? other)
    {
        return other is Holding holding && Id == holding.Id;
    }

    /// <summary>
    ///     Determines whether two Holding objects are equal.
    /// </summary>
    /// <param name="lvalue">The first Holding object to compare.</param>
    /// <param name="rvalue">The second Holding object to compare.</param>
    /// <returns>
    ///     <c>true</c> if the specified Holding objects are equal;
    ///     otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     If both are null, they are considered equal.
    ///     This equality comparison is based on the ID's of the Holding objects.
    /// </remarks>
    public static bool operator ==(Holding? lvalue, Holding? rvalue)
    {
        return (lvalue is null && rvalue is null) ||
               (lvalue is not null && lvalue.Equals(rvalue));
    }

    /// <summary>
    ///     Determines whether two Holding objects are not equal.
    /// </summary>
    /// <param name="lvalue">The first Holding object to compare.</param>
    /// <param name="rvalue">The second Holding object to compare.</param>
    /// <returns>
    ///     <c>true</c> if the specified Holding objects are not equal;
    ///     otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     This inequality comparison is based on the ID's of the Holding objects.
    ///     If both are null, they are considered equal.
    /// </remarks>
    public static bool operator !=(Holding? lvalue, Holding? rvalue)
    {
        return !(lvalue == rvalue);
    }

    /// <summary>
    ///     Serves as a hash function for the Holding class.
    /// </summary>
    /// <returns>
    ///     A hash code for the current Holding object.
    /// </returns>
    /// <remarks>
    ///     The hash code is generated based on the ID of the Holding object.
    /// </remarks>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

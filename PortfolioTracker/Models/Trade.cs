using System;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Models;

public class Trade : IEquatable<Trade>
{
    /// <summary>
    ///     Initializes a new instance of the Trade class with specified parameters.
    /// </summary>
    /// <param name="name">The name of the asset.</param>
    /// <!--<param name="ticker">The stock ticker symbol associated with the trade.</param>-->
    /// <param name="isBuyOrder">A boolean indicating whether the trade is a buy order (true) or a sell order (false).</param>
    /// <param name="date">The date when the trade occurred.</param>
    /// <param name="quantity">The quantity of the asset involved in the trade.</param>
    /// <param name="price">The price per unit of the asset in the trade.</param>
    /// <param name="tax">The tax amount associated with the trade.</param>
    /// <param name="commission">The commission amount associated with the trade.</param>
    public Trade(string name, string ticker, bool orderType, DateOnly date, string quantity, string price, string tax,
                 string commission, CurrencyModel currency)
    {
        Name = name;
        Ticker = ticker;
        IsBuyOrder = orderType;
        Date = date;
        decimal quantityValue, priceValue, taxValue, commissionValue;
        decimal.TryParse(quantity, out quantityValue);
        decimal.TryParse(price, out priceValue);
        decimal.TryParse(tax, out taxValue);
        decimal.TryParse(commission, out commissionValue);

        Quantity = quantityValue;
        Price = priceValue;
        Tax = taxValue;
        Commission = commissionValue;
        Currency = currency;
    }

    public Trade(int id, string name, string ticker, bool orderType, DateOnly date, decimal quantity, decimal price,
                 decimal tax, decimal commission, CurrencyModel currency)
    {
        Id = id;
        Name = name;
        Ticker = ticker;
        IsBuyOrder = orderType;
        Date = date;
        Quantity = quantity;
        Price = price;
        Tax = tax;
        Commission = commission;
        Currency = currency;
    }

    /// <summary>
    ///     Unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     The name of the asset traded.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     3 or letter ticker symbol
    /// </summary>
    public string Ticker { get; set; }

    //// <summary>
    ///// The ticker symbol of the asset traded.
    ///// </summary>
    //public string Ticker { get; set; } = string.Empty;

    /// <summary>
    ///     Indicates whether the transaction is a buy order (true) or a sell order (false).
    /// </summary>
    public bool IsBuyOrder { get; set; }

    /// <summary>
    ///     The date when the trade took place.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    ///     The amount of shares traded.
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    ///     The price per share.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    ///     Total tax paid on the trade
    /// </summary>
    public decimal Tax { get; set; }

    /// <summary>
    ///     Total commission paid on the trade
    /// </summary>
    public decimal Commission { get; set; }

    /// <summary>
    ///     The currency of the payment
    /// </summary>
    public CurrencyModel Currency { get; set; }

    /// <summary>
    ///     The total value of this transaction, based on the rate.
    ///     Calculated as the product of the rate and the quantity.
    /// </summary>
    public decimal Value => Price * Quantity;

    /// <summary>
    ///     Determines whether the current Trade object is equal to another object based on their IDs.
    /// </summary>
    /// <param name="obj">The object to compare with the current Trade object.</param>
    /// <returns>
    ///     <c>true</c> if the specified object is equal to the current Trade object; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     Trades are considered equal if their IDs match.
    /// </remarks>
    public bool Equals(Trade? obj)
    {
        return obj is Trade trade && Id == trade.Id;
    }

    /// <summary>
    ///     Determines whether two Trade objects are equal based on their IDs.
    /// </summary>
    /// <param name="lvalue">The first Trade object to compare.</param>
    /// <param name="rvalue">The second Trade object to compare.</param>
    /// <returns>
    ///     <c>true</c> if the specified Trade objects are equal; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>Two null Trades are considered equal.</remarks>
    public static bool operator ==(Trade? lvalue, Trade? rvalue)
    {
        if (lvalue is null && rvalue is null)
        {
            return true;
        }

        return lvalue is not null && lvalue.Equals(rvalue);
    }

    /// <summary>
    ///     Determines whether two Trade objects are not equal based on their IDs.
    /// </summary>
    /// <param name="lvalue">The first Trade object to compare.</param>
    /// <param name="rvalue">The second Trade object to compare.</param>
    /// <returns>
    ///     <c>true</c> if the specified Trade objects are not equal; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>Two null Trades are considered equal.</remarks>
    public static bool operator !=(Trade? lvalue, Trade? rvalue)
    {
        return !(lvalue == rvalue);
    }

    /// <summary>
    ///     Determines whether the current Trade object is equal to another object based on their IDs.
    /// </summary>
    /// <param name="obj">The object to compare with the current Trade object.</param>
    /// <returns>
    ///     <c>true</c> if the specified object is equal to the current Trade object; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     Trades are considered equal if their IDs match.
    /// </remarks>
    public override bool Equals(object? obj)
    {
        return obj is Trade trade && Id == trade.Id;
    }

    // <summary>
    /// Serves as a hash function for the Trade class based on its ID.
    /// </summary>
    /// <returns>
    ///     A hash code for the current Trade object based on its ID.
    /// </returns>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

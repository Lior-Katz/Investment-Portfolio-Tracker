using PortfolioTracker.Models;
using System;

namespace PortfolioTracker.ViewModels
{
	public class TradeViewModel : ViewModelBase
	{
		/// <summary>
		/// The Trade object that the ViewModel represents.
		/// </summary>
		private readonly Trade _trade;

		/// <summary>
		/// Unique identifier.
		/// </summary>
		public int Id => _trade.Id;

		/// <summary>
		/// The name of the asset traded.
		/// </summary>
		public string Name => _trade.Name;

		/// <summary>
		/// 3 or 4 letter ticker symbol
		/// </summary>
		public string Ticker => _trade.Ticker;

		///// <summary>
		///// The ticker symbol of the asset traded.
		///// </summary>
		//public string Ticker => _trade.Ticker;

		/// <summary>
		/// A string representation of the transaction type- <c>Buy</c> or <c>Sell</c>.
		/// </summary>
		public string OrderType => _trade.OrderType;

		/// <summary>
		/// The date when the trade took place.
		/// </summary>
		public DateOnly Date => _trade.Date;

		/// <summary>
		/// The amount of shares traded.
		/// </summary>
		public decimal Quantity => _trade.Quantity;

		/// The price per share.
		/// </summary>
		public decimal Price => _trade.Price;

		/// <summary>
		/// Total tax paid on the trade
		/// </summary>
		public decimal Tax => _trade.Tax;

		/// <summary>
		/// Total commission paid on the trade
		/// </summary>
		public decimal Commission => _trade.Commission;

		/// <summary>
		/// The currency of the payment
		/// </summary>
		public CurrencyModel Currency => _trade.Currency;

		/// <summary>
		/// The total value of this transaction, based on the rate.
		/// Calculated as the product of the rate and the quantity.
		/// </summary>
		public decimal Value => _trade.Value;

		/// <summary>
		/// Initializes a new instance of TradeViewModel that represents the holding.
		/// </summary>
		/// <param name="trade">The trade to represent.</param>
		public TradeViewModel(Trade trade) => this._trade = trade;
	}
}
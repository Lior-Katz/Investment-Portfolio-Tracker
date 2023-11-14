using PortfolioTracker.Models;
using System;

namespace PortfolioTracker.ViewModels
{
	internal class TradeViewModel : ViewModelBase
	{
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
		/// The ticker symbol of the asset traded.
		/// </summary>
		public string Ticker => _trade.Ticker;

		public string OrderType
		{
			get
			{
				if (_trade.IsBuyOrder)
				{
					return "Buy";
				}
				return "Sell";
			}
		}

		/// <summary>
		/// The date when the trade took place.
		/// </summary>
		public DateTime Date => _trade.Date;
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

		public decimal Value => _trade.Value;
		public TradeViewModel(Trade trade) => this._trade = trade;
	}
}
using PortfolioTracker.Models;
using System;
using System.Collections.Generic;

namespace PortfolioTracker.ViewModels
{
	public class HoldingViewModel : ViewModelBase
	{
		/// <summary>
		/// The holding object that the ViewModel represents.
		/// </summary>
		private readonly Holding _holding;

		/// <summary>
		/// Unique identifier
		/// </summary>
		public int Id => _holding.Id;

		/// <summary>
		/// Asset Name
		/// </summary>
		public string Name => _holding.Name;

		/// <summary>
		/// The 3- or 4-letter symbol of the asset
		/// </summary>
		public string Ticker => _holding.Ticker;

		/// <summary>
		/// Number of shares held.
		/// </summary>
		public decimal Quantity => _holding.Quantity;

		/// <summary>
		/// List of all trades of this asset.
		/// </summary>
		public List<Trade> Trades => _holding.Trades;

		/// <summary>
		/// Initial date when this asset was aquired.
		/// </summary>
		public DateOnly AcquisitionDate => _holding.AcquisitionDate;

		/// <summary>
		/// The periodical payout of this asset.
		/// </summary>
		public Payout? Payout => _holding.Payout;

		/// <summary>
		/// The type of investment vehicle.
		/// </summary>
		/// <example>
		/// Company stock, index fund, bond, etc.
		/// </example>
		public string Type => _holding.Type;

		/// <summary>
		/// The sector of this asset.
		/// </summary>
		public string Sector => _holding.Sector;

		/// <summary>
		/// The market where this asset is traded.
		/// </summary>
		public string Market => _holding.Market;

		/// <summary>
		/// Average rate per unit of the currently owned units of this holding.
		/// Calculated as the differnce between all spending on this holding and all income from selling, all divided by the quantity.
		/// Disregards tax, commissions, and payouts.
		/// </summary>
		public decimal AveragePrice => _holding.AveragePrice;

		/// <summary>
		/// The current market price of this asset.
		/// </summary>
		public decimal CurrentPrice => _holding.CurrentPrice;

		/// <summary>
		/// The total investment value of this asset, based on its current market price.
		/// Calculated as the product of the current price and the quantity.
		/// </summary>
		public decimal Value => _holding.Value;

		/// <summary>
		/// The daily change in the value of the asset, expressed as a percentage of its current total value.
		/// </summary>
		public decimal DailyChangePercentage => _holding.DailyChangePercentage;

		/// <summary>
		/// The daily change in the value of the asset.
		/// </summary>
		public decimal DailyChange => _holding.DailyChange;

		/// <summary>
		/// Initializes a new instance of HoldingViewModel that represents the holding.
		/// </summary>
		/// <param name="holding">The holding to represent.</param>
		public HoldingViewModel(Holding holding)
		{
			_holding = holding;
		}

		public Holding ToHolding() => _holding;
	}
}

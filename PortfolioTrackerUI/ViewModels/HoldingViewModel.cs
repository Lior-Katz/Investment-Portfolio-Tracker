using PortfolioTracker.Models;
using System;
using System.Collections.Generic;

namespace PortfolioTracker.ViewModels
{
	public class HoldingViewModel : ViewModelBase
	{
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
		public DateOnly AquisitionDate => _holding.AquisitionDate;
		/// <summary>
		/// The periodical payout of this asset.
		/// </summary>
		public Payout Payout => _holding.Payout;
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
		public decimal AveragePrice => _holding.AveragePrice;
		public decimal CurrentPrice => _holding.CurrentPrice;
		public decimal Value => _holding.Value;
		public decimal DailyChangePercentage => _holding.DailyChangePercentage;
		public decimal DailyChange => _holding.DailyChange;
		public decimal PercentofPortfolio => _holding.PercentofPortfolio;

		public HoldingViewModel(Holding holding)
		{
			_holding = holding;
		}

	}
}

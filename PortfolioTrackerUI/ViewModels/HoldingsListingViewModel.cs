using PortfolioTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PortfolioTracker.ViewModels
{
	/// <summary>
	/// ViewModel representing the list of all holdings.
	/// </summary>
	public class HoldingsListingViewModel : ViewModelBase
	{
		/// <summary>
		/// Gets the collection of holdings as a sequence of HoldingViewModels.
		/// </summary>
		private readonly ObservableCollection<HoldingViewModel> _holdings;
		public IEnumerable<HoldingViewModel> Holdings => _holdings;

		/// <summary>
		/// Initializes a new instance of the <see cref="HoldingsListingViewModel"/> class, with an empty collection of holdings.
		/// Populates the holdings with sample data for demonstration purposes.
		/// </summary>
		public HoldingsListingViewModel()
		{
			_holdings = new ObservableCollection<HoldingViewModel>();
			DateOnly date = new DateOnly(2023, 11, 14);
			_holdings.Add(new HoldingViewModel(new Holding("google", "GOOG", 25, date, 0, 0, 0, 3, "Stock", "Oil", "US")));
			_holdings.Add(new HoldingViewModel(new Holding("amazon", "AMZ", 300, date, 0, 0, 0, 3, "Stock", "Oil", "US")));
			_holdings.Add(new HoldingViewModel(new Holding("microsoft", "MSFT", 5, date, 0, 0, 0, 3, "Stock", "Oil", "US")));
		}


	}
}

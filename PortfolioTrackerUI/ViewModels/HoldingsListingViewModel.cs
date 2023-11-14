using PortfolioTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PortfolioTracker.ViewModels
{
	public class HoldingsListingViewModel : ViewModelBase
	{

		private readonly ObservableCollection<HoldingViewModel> _holdings;

		public IEnumerable<HoldingViewModel> Holdings => _holdings;

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

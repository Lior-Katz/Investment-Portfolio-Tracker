using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PortfolioTracker.ViewModels
{
	class TransactionHistoryViewModel : ViewModelBase
	{
		private readonly ObservableCollection<TradeViewModel> _transactions;

		public IEnumerable<TradeViewModel> Transactions => _transactions;

		/// <summary>
		/// Initializes a new instance of the <see cref="TransactionHistoryViewModel"/> class, with an ObservableCollection of TradeViewModels.
		/// <param name="holdingViewModels">An ObservableCollection of TradeViewModels to be shown on the view.</param>
		/// </summary>
		public TransactionHistoryViewModel(ObservableCollection<TradeViewModel> tradeViewModels)
		{
			_transactions = tradeViewModels;
		}
	}
}

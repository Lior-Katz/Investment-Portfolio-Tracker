using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PortfolioTracker.ViewModels
{
	class TransactionHistoryViewModel : ViewModelBase
	{
		private readonly ObservableCollection<TradeViewModel> _transactions;

		public IEnumerable<TradeViewModel> Transactions => _transactions;

		public TransactionHistoryViewModel()
		{
			_transactions = new ObservableCollection<TradeViewModel>();
		}
	}
}

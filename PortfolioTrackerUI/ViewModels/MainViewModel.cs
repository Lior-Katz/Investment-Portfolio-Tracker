using PortfolioTracker.Models;

namespace PortfolioTracker.ViewModels
{
	class MainViewModel : ViewModelBase
	{
		public ViewModelBase CurrentViewModel { get; }

		public MainViewModel(Portfolio portfolio)
		{
			CurrentViewModel = new TransactionHistoryViewModel();
		}

	}
}

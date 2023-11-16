using PortfolioTracker.Stores;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Commands
{
	public class NavigateToTransactionHistoryCommand : CommandsBase
	{
		private readonly NavigationStore _navigationStore;

		public NavigateToTransactionHistoryCommand(NavigationStore navigationStore)
		{
			this._navigationStore = navigationStore;
		}

		public override void Execute(object? parameter)
		{
			_navigationStore.CurrentViewModel = new TransactionHistoryViewModel();
		}
	}
}

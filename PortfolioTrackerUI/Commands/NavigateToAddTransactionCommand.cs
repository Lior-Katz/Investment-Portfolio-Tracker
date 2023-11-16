using PortfolioTracker.Stores;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Commands
{
	public class NavigateToAddTransactionCommand : CommandsBase
	{
		private readonly NavigationStore _navigationStore;

		public NavigateToAddTransactionCommand(NavigationStore navigationStore)
		{
			this._navigationStore = navigationStore;
		}

		public override void Execute(object? parameter)
		{
			_navigationStore.CurrentViewModel = new AddTransactionViewModel();
		}
	}
}

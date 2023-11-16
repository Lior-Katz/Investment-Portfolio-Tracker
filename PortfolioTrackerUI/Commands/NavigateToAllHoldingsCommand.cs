using PortfolioTracker.Stores;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Commands
{
	public class NavigateToAllHoldingsCommand : CommandsBase
	{
		private readonly NavigationStore _navigationStore;

		public NavigateToAllHoldingsCommand(NavigationStore navigationStore)
		{
			_navigationStore = navigationStore;
		}
		public override void Execute(object? parameter)
		{
			_navigationStore.CurrentViewModel = new HoldingsListingViewModel();
		}

	}
}

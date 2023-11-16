using PortfolioTracker.Stores;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Commands
{
	public class NavigateToDistributionsCommand : CommandsBase
	{
		private readonly NavigationStore _navigationStore;

		public NavigateToDistributionsCommand(NavigationStore navigationStore)
		{
			this._navigationStore = navigationStore;
		}

		public override void Execute(object? parameter)
		{
			this._navigationStore.CurrentViewModel = new DistributionsViewModel();
		}
	}
}

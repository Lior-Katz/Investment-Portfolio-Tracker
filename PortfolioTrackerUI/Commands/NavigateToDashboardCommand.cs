using PortfolioTracker.Stores;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Commands;
public class NavigateToDashboardCommand : CommandsBase
{
	private readonly NavigationStore _navigationStore;

	public NavigateToDashboardCommand(NavigationStore navigationStore)
	{
		_navigationStore = navigationStore;
	}
	public override void Execute(object? parameter)
	{
		_navigationStore.CurrentViewModel = new DashboardViewModel(_navigationStore/*._portfolio.MostInfluentialHoldings*/);
	}
}

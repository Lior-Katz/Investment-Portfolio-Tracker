using PortfolioTracker.Commands;
using PortfolioTracker.Stores;
using System.Windows.Input;

namespace PortfolioTracker.ViewModels
{
	public class BannerViewModel : ViewModelBase
	{
		public ICommand NavigateToDashboardCommand { get; }

		public BannerViewModel(NavigationStore navigationStore, PortfolioViewModel portfolioViewModel)
		{
			this.NavigateToDashboardCommand = new NavigateCommand<DashboardViewModel>(navigationStore,
				() => new DashboardViewModel(portfolioViewModel, navigationStore));
		}
	}
}

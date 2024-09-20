using PortfolioTracker.Commands;
using PortfolioTracker.Stores;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;

namespace PortfolioTracker.ViewModels
{
	public class BannerViewModel : ViewModelBase
	{
		private readonly PortfolioViewModel _portfolioViewModel;
		public PortfolioViewModel PortfolioViewModel => _portfolioViewModel;

		public ICommand NavigateToDashboardCommand { get; }



		public BannerViewModel(NavigationStore navigationStore, PortfolioViewModel portfolioViewModel)
		{
			_portfolioViewModel = portfolioViewModel;
			this.NavigateToDashboardCommand = new NavigateCommand<DashboardViewModel>(navigationStore,
				() => App.AppHost.Services.GetRequiredService<DashboardViewModel>());
		}

	}
}

namespace PortfolioTracker.ViewModels
{
	class MainViewModel : ViewModelBase
	{
		public ViewModelBase CurrentViewModel { get; }

		public MainViewModel(PortfolioViewModel portfolio)
		{
			CurrentViewModel = new DashboardViewModel(portfolio.MostInfluentialHoldings);
		}

	}
}

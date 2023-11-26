using PortfolioTracker.Services;
using PortfolioTracker.Stores;
using PortfolioTracker.ViewModels;
using System.Windows;

namespace PortfolioTracker
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly PortfolioViewModel _portfolio;
		public App()
		{
			_portfolio = DataService.RetrievePortfolio(1);

			// TODO: populate with data from db
		}
		protected override void OnStartup(StartupEventArgs e)
		{
			NavigationStore navigationStore = new NavigationStore();
			navigationStore.CurrentViewModel = new DashboardViewModel(_portfolio, navigationStore);

			if (DataService.isPortfoliosEmpty())
			{
				_portfolio.Id = DataService.WriteData(_portfolio);
			}

			MainWindow = new MainWindow()
			{
				DataContext = new MainViewModel(navigationStore, _portfolio)
			};

			MainWindow.Show();

			base.OnStartup(e);
		}

		//private PortfolioViewModel initPortfolio(string name)
		//{

		//	Portfolio portfolio = 



		//}
	}
}

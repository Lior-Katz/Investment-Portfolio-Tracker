using PortfolioTracker.Models;
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
			_portfolio = new PortfolioViewModel(new Portfolio("MyPortfolio"));


			// TODO: populate with data from db
		}
		protected override void OnStartup(StartupEventArgs e)
		{
			NavigationStore navigationStore = new NavigationStore();
			navigationStore.CurrentViewModel = new DashboardViewModel(navigationStore);

			MainWindow = new MainWindow()
			{
				DataContext = new MainViewModel(navigationStore)
			};

			MainWindow.Show();

			base.OnStartup(e);
		}
	}
}

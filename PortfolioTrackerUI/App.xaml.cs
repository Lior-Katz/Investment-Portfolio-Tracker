using PortfolioTracker.Models;
using PortfolioTracker.ViewModels;
using System.Windows;

namespace PortfolioTracker
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly Portfolio _portfolio;
		public App()
		{
			_portfolio = new Portfolio("MyPostrfolio");
		}
		protected override void OnStartup(StartupEventArgs e)
		{
			MainWindow = new MainWindow()
			{
				DataContext = new MainViewModel(_portfolio)
			};

			MainWindow.Show();

			base.OnStartup(e);
		}
	}
}

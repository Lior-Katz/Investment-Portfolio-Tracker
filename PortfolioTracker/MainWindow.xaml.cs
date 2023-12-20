using System.Windows;

namespace PortfolioTracker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();


			//// Subscribe to SwitchToHoldingsScreenRequested event in DashboardView.
			//DashboardView.SwitchToHoldingsScreenRequested += MainScreen_SwitchToHoldingsScreenRequested;

			//// Subscribe to SwitchToTransactionScreenScreenRequested event in DashboardView.
			//DashboardView.SwitchToTransactionHistoryScreenRequested += MainScreen_SwitchToTransactionHistoryScreenRequested;

			//// Subscribe to SwitchToAddTransactionScreenScreenRequested event in DashboardView.
			//DashboardView.SwitchToAddTransactionScreenRequested += MainScreen_SwitchToAddTransactionScreenRequested;

			//// Subscribe to SwitchToDistributionsScreenScreenRequested event in DashboardView.
			//DashboardView.SwitchToDistributionsScreenRequested += MainScreen_SwitchToDistributionsScreenRequested;

			//// Subscribe to SwitchToMainScreenRequested event in Banner
			//Banner.SwitchToMainScreenRequested += Banner_SwitchToDistributionsScreenRequested;

			//// Subscribe to SwitchToAddTransactionScreenRequested event in TransactionHistoryScreen
			//TransactionHistoryScreen.SwitchToAddTransactionScreenRequested += TransactionHistoryScreen_SwitchToAddTransactionScreenRequested;

			//AddTransactionScreen.SwitchToMainScreenRequested += AddTransactionScreen_SwitchToMainScreenRequested;
		}

		//public void MainScreen_SwitchToHoldingsScreenRequested(object sender, EventArgs e)
		//{
		//	HoldingsScreen.Visibility = Visibility.Visible;
		//}

		//public void MainScreen_SwitchToTransactionHistoryScreenRequested(object sender, EventArgs e)
		//{
		//	TransactionHistoryScreen.Visibility = Visibility.Visible;
		//}

		//public void MainScreen_SwitchToAddTransactionScreenRequested(object sender, EventArgs e)
		//{
		//	AddTransactionScreen.Visibility = Visibility.Visible;
		//}

		//public void MainScreen_SwitchToDistributionsScreenRequested(object sender, EventArgs e)
		//{
		//	DistributionsScreen.Visibility = Visibility.Visible;
		//}

		//public void Banner_SwitchToDistributionsScreenRequested(object sender, EventArgs e)
		//{
		//	HoldingsScreen.Visibility = Visibility.Collapsed;
		//	TransactionHistoryScreen.Visibility = Visibility.Collapsed;
		//	AddTransactionScreen.Visibility = Visibility.Collapsed;
		//	DistributionsScreen.Visibility = Visibility.Collapsed;

		//	DashboardView.Visibility = Visibility.Visible;
		//}

		//public void TransactionHistoryScreen_SwitchToAddTransactionScreenRequested(object sender, EventArgs e)
		//{
		//	AddTransactionScreen.Visibility = Visibility.Visible;
		//}

		//public void AddTransactionScreen_SwitchToMainScreenRequested(object sender, EventArgs e)
		//{
		//	DashboardView.Visibility = Visibility.Visible;
		//}
	}
}

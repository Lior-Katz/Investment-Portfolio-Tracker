using PortfolioTrackerUI.View.Screens;
using System;
using System.Windows;

namespace PortfolioTrackerUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			// Subscribe to SwitchToHoldingsScreenRequested event in MainScreen.
			MainScreen.SwitchToHoldingsScreenRequested += MainScreen_SwitchToHoldingsScreenRequested;

			// Subscribe to SwitchToTransactionScreenScreenRequested event in MainScreen.
			MainScreen.SwitchToTransactionHistoryScreenRequested += MainScreen_SwitchToTransactionHistoryScreenRequested;

			// Subscribe to SwitchToAddTransactionScreenScreenRequested event in MainScreen.
			MainScreen.SwitchToAddTransactionScreenRequested += MainScreen_SwitchToAddTransactionScreenRequested;

			// Subscribe to SwitchToDistributionsScreenScreenRequested event in MainScreen.
			MainScreen.SwitchToDistributionsScreenRequested += MainScreen_SwitchToDistributionsScreenRequested;
		}

		public void MainScreen_SwitchToHoldingsScreenRequested(object sender, EventArgs e)
		{
			HoldingsScreen.Visibility = Visibility.Visible;
		}

		public void MainScreen_SwitchToTransactionHistoryScreenRequested(object sender, EventArgs e)
		{
			TransactionHistoryScreen.Visibility = Visibility.Visible;
		}

		public void MainScreen_SwitchToAddTransactionScreenRequested(object sender, EventArgs e)
		{
			AddTransactionScreen.Visibility = Visibility.Visible;
		}

		public void MainScreen_SwitchToDistributionsScreenRequested(object sender, EventArgs e)
		{
			DistributionsScreen.Visibility = Visibility.Visible;
		}
	}
}

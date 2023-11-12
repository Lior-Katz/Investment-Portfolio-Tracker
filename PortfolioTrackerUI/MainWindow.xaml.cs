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

			// subscribe to SwitchToHoldingsScreenRequested event in MainScreen
			MainScreen.SwitchToHoldingsScreenRequested += MainScreen_SwitchToHoldingsScreenRequested;
		}

		public void MainScreen_SwitchToHoldingsScreenRequested(object sender, EventArgs e)
		{
			HoldingsScreen.Visibility = Visibility.Visible;
		}
	}
}

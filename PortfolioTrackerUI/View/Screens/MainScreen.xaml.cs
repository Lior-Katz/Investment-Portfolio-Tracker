using System;
using System.Windows.Controls;

namespace PortfolioTrackerUI.View.Screens
{
	/// <summary>
	/// Interaction logic for MainScreen.xaml
	/// </summary>
	public partial class MainScreen : UserControl
	{
		public event EventHandler SwitchToHoldingsScreenRequested;
		public MainScreen()
		{
			InitializeComponent();
		}

		private void btnAllHoldings_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			myMainScreen.Visibility = System.Windows.Visibility.Collapsed;
			SwitchToHoldingsScreenRequested?.Invoke(this, EventArgs.Empty);
		}
    }
}

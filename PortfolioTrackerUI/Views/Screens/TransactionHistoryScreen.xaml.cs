using System;
using System.Windows.Controls;

namespace PortfolioTrackerUI.View.Screens
{
	public partial class TransactionHistoryScreen : UserControl
	{
		public event EventHandler SwitchToAddTransactionScreenRequested;
		public TransactionHistoryScreen()
		{
			InitializeComponent();
		}

		//private void btnAddTransaction_Click(object sender, System.Windows.RoutedEventArgs e)
		//{
		//	myTransactionHistoryScreen.Visibility = System.Windows.Visibility.Collapsed;
		//	SwitchToAddTransactionScreenRequested?.Invoke(this, e);
		//}
	}
}

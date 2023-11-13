using System;
using System.Windows.Controls;

namespace PortfolioTrackerUI.View.Screens
{
	/// <summary>
	/// Interaction logic for AddTransactionScreen.xaml
	/// </summary>
	public partial class AddTransactionScreen : UserControl
	{
		public event EventHandler SwitchToMainScreenRequested;
		public AddTransactionScreen()
		{
			InitializeComponent();
		}

		private void btnConfirmTransactionAdd_Click(object sender, System.Windows.RoutedEventArgs e)
		{

			myAddTransactionScreen.Visibility = System.Windows.Visibility.Collapsed;
			SwitchToMainScreenRequested?.Invoke(this, new EventArgs());
			//TODO- save to database
		}

		//private bool isValidForm(string name, string quantity, string rate, string commission, string tax)
		//{
		//	if (name == string.Empty)
		//	{

		//		return false;
		//	}
		//}
	}
}

using System.Windows;

namespace PortfolioTracker.Views.UserControls
{
	/// <summary>
	/// Interaction logic for AddTransactionDialog.xaml
	/// </summary>
	public partial class AddTransactionDialog : Window
	{
		public AddTransactionDialog()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}

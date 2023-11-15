using System;
using System.Windows.Controls;

namespace PortfolioTracker.Views.UserControls
{
	/// <summary>
	/// Interaction logic for Banner.xaml
	/// </summary>
	public partial class Banner : UserControl
	{
		public event EventHandler SwitchToMainScreenRequested;

		public Banner()
		{
			InitializeComponent();
		}

		private void btnGoToMainScreen_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			SwitchToMainScreenRequested?.Invoke(this, e);
		}
	}
}

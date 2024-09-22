using System;
using System.Windows.Controls;

namespace PortfolioTracker.Views.Screens;

public partial class TransactionHistoryScreen : UserControl
{
    public TransactionHistoryScreen()
    {
        InitializeComponent();
    }

    public event EventHandler SwitchToAddTransactionScreenRequested;

    //private void btnAddTransaction_Click(object sender, System.Windows.RoutedEventArgs e)
    //{
    //	myTransactionHistoryScreen.Visibility = System.Windows.Visibility.Collapsed;
    //	SwitchToAddTransactionScreenRequested?.Invoke(this, e);
    //}
}

using System;
using System.Windows.Controls;

namespace PortfolioTracker.Views.Screens;

/// <summary>
///     Interaction logic for DashboardView.xaml
/// </summary>
public partial class DashboardView : UserControl
{
    public DashboardView()
    {
        InitializeComponent();
    }

    public event EventHandler SwitchToHoldingsScreenRequested;
    public event EventHandler SwitchToTransactionHistoryScreenRequested;
    public event EventHandler SwitchToAddTransactionScreenRequested;
    public event EventHandler SwitchToDistributionsScreenRequested;

    //private void btnAllHoldings_Click(object sender, System.Windows.RoutedEventArgs e)
    //{
    //	myMainScreen.Visibility = System.Windows.Visibility.Collapsed;
    //	SwitchToHoldingsScreenRequested?.Invoke(this, EventArgs.Empty);
    //}

    //private void btnAddTransaction_Click(object sender, System.Windows.RoutedEventArgs e)
    //{
    //	myMainScreen.Visibility = System.Windows.Visibility.Collapsed;
    //	SwitchToAddTransactionScreenRequested?.Invoke(this, EventArgs.Empty);
    //}

    //private void btnDistributions_Click(object sender, System.Windows.RoutedEventArgs e)
    //{
    //	myMainScreen.Visibility = System.Windows.Visibility.Collapsed;
    //	SwitchToDistributionsScreenRequested?.Invoke(this, EventArgs.Empty);
    //}

    //private void btnTransactionHistory_Click(object sender, System.Windows.RoutedEventArgs e)
    //{
    //	myMainScreen.Visibility = System.Windows.Visibility.Collapsed;
    //	SwitchToTransactionHistoryScreenRequested?.Invoke(this, EventArgs.Empty);
    //}
}

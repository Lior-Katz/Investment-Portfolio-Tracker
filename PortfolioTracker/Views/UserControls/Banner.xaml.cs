using System;
using System.Windows;
using System.Windows.Controls;

namespace PortfolioTracker.Views.UserControls;

/// <summary>
///     Interaction logic for Banner.xaml
/// </summary>
public partial class Banner : UserControl
{
    public Banner()
    {
        InitializeComponent();
    }

    public event EventHandler SwitchToMainScreenRequested;

    private void btnGoToMainScreen_Click(object sender, RoutedEventArgs e)
    {
        SwitchToMainScreenRequested?.Invoke(this, e);
    }
}

using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using PortfolioTracker.Commands;
using PortfolioTracker.Stores;

namespace PortfolioTracker.ViewModels;

public class BannerViewModel : ViewModelBase
{
    public BannerViewModel(NavigationStore navigationStore, PortfolioViewModel portfolioViewModel)
    {
        PortfolioViewModel = portfolioViewModel;
        NavigateToDashboardCommand = new NavigateCommand<DashboardViewModel>(navigationStore,
                                                                             () => App.AppHost.Services
                                                                                 .GetRequiredService<
                                                                                     DashboardViewModel>());
    }

    public PortfolioViewModel PortfolioViewModel { get; }

    public ICommand NavigateToDashboardCommand { get; }
}

using System;
using PortfolioTracker.Stores;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Commands;

public class NavigateCommand<TViewModel> : CommandsBase
    where TViewModel : ViewModelBase
{
    private readonly Func<TViewModel> _createViewModel;
    private readonly NavigationStore _navigationStore;

    public NavigateCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel)
    {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
    }

    public override void Execute(object? parameter)
    {
        _navigationStore.CurrentViewModel = _createViewModel();
    }
}

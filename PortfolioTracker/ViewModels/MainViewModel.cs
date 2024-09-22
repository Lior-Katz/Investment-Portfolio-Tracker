using PortfolioTracker.Stores;

namespace PortfolioTracker.ViewModels;

internal class MainViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public MainViewModel(NavigationStore navigationStore, PortfolioViewModel portfolioViewModel)
    {
        _navigationStore = navigationStore;
        BannerViewModel = new BannerViewModel(navigationStore, portfolioViewModel);

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    public ViewModelBase BannerViewModel { get; }

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}

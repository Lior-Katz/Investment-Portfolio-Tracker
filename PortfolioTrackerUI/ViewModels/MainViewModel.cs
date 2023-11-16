using PortfolioTracker.Stores;

namespace PortfolioTracker.ViewModels
{
	class MainViewModel : ViewModelBase
	{
		private ViewModelBase _bannerViewModel;
		public ViewModelBase BannerViewModel => _bannerViewModel;

		private readonly NavigationStore _navigationStore;

		public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
		public MainViewModel(NavigationStore navigationStore)
		{
			_navigationStore = navigationStore;
			_bannerViewModel = new BannerViewModel(navigationStore);

			_navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
		}

		private void OnCurrentViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}
	}
}

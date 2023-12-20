using PortfolioTracker.Stores;
using PortfolioTracker.ViewModels;
using System;

namespace PortfolioTracker.Commands
{
	public class NavigateCommand<TViewModel> : CommandsBase
		where TViewModel : ViewModelBase
	{
		private readonly NavigationStore _navigationStore;
		private readonly Func<TViewModel> _createViewModel;

		public NavigateCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel)
		{
			this._navigationStore = navigationStore;
			this._createViewModel = createViewModel;
		}

		public override void Execute(object? parameter)
		{
			_navigationStore.CurrentViewModel = _createViewModel();
		}
	}
}

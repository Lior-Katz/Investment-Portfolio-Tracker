using PortfolioTracker.ViewModels;
using System;

namespace PortfolioTracker.Stores;
public class NavigationStore
{
	public event Action CurrentViewModelChanged;

	private ViewModelBase _currentViewModel;
	public ViewModelBase CurrentViewModel
	{
		get => _currentViewModel;

		set
		{
			_currentViewModel = value;
			OnCurrentViewModelChanged();
		}
	}

	private void OnCurrentViewModelChanged()
	{
		CurrentViewModelChanged?.Invoke();
	}

	//public PortfolioViewModel _portfolio { get; }

}

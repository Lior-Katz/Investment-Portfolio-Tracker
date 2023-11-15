using System.Collections.ObjectModel;

namespace PortfolioTracker.ViewModels;
public class DistributionsViewModel : ViewModelBase
{
	private readonly ObservableCollection<HoldingViewModel> _holdings;
	public ObservableCollection<HoldingViewModel> Holdings;

	public DistributionsViewModel(ObservableCollection<HoldingViewModel> holdingViewModels)
	{
		this._holdings = holdingViewModels;
	}
}

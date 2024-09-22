using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PortfolioTracker.ViewModels;

/// <summary>
///     ViewModel representing the list of all holdings.
/// </summary>
public class HoldingsListingViewModel : ViewModelBase
{
	/// <summary>
	///     Gets the collection of holdings as a sequence of HoldingViewModels.
	/// </summary>
	private readonly ObservableCollection<HoldingViewModel> _holdings;

	/// <summary>
	///     Initializes a new instance of the <see cref="HoldingsListingViewModel" /> class, with an ObservableCollection of
	///     HoldingViewModels.
	///     <param name="holdingViewModels">An ObservableCollection of HoldingViewModels to be shown on the view.</param>
	/// </summary>
	public HoldingsListingViewModel(PortfolioViewModel portfolioViewModel)
    {
        _holdings = portfolioViewModel.Holdings;
    }

    public IEnumerable<HoldingViewModel> Holdings => _holdings;
}

using System.Collections.ObjectModel;

namespace PortfolioTracker.ViewModels;
public class DashboardViewModel : ViewModelBase
{
	private readonly ObservableCollection<HoldingViewModel> _mostInfluentialholdings;
	public ObservableCollection<HoldingViewModel> MostInfluentialHoldings => _mostInfluentialholdings;

	public CurrencyViewModel SelectedCurrency { get; } = new CurrencyViewModel();

	public DashboardViewModel(ObservableCollection<HoldingViewModel> mostInfluential_holdingViewModels)
	{
		this._mostInfluentialholdings = mostInfluential_holdingViewModels;
	}

	DashboardViewModel(ObservableCollection<HoldingViewModel> mostInfluential_holdingViewModels, CurrencyViewModel selectedCurrency)
	{
		this._mostInfluentialholdings = mostInfluential_holdingViewModels;
		this.SelectedCurrency = selectedCurrency;
	}

}

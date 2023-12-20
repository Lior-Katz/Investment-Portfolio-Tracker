using PortfolioTracker.Commands;
using PortfolioTracker.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PortfolioTracker.ViewModels;
public class DashboardViewModel : ViewModelBase
{
	private readonly ObservableCollection<HoldingViewModel> _mostInfluentialHoldings;
	public ObservableCollection<HoldingViewModel> MostInfluentialHoldings => _mostInfluentialHoldings;

	public ICommand NavigateToAllHoldingsCommand { get; }

	public ICommand NavigateToTransactionHistoryCommand { get; }

	public ICommand NavigateToAddTransactionCommand { get; }

	public ICommand NavigateToDistributionsCommand { get; }
	public CurrencyModel SelectedCurrency { get; }

	public LineGraphViewModel HistoricValuesLineGraph { get; }

	public DashboardViewModel(PortfolioViewModel portfolioViewModel, NavigationStore navigationStore)
	{
		this._mostInfluentialHoldings = portfolioViewModel.MostInfluentialHoldings;

		NavigateToAllHoldingsCommand = new NavigateCommand<HoldingsListingViewModel>(navigationStore, () => new HoldingsListingViewModel(portfolioViewModel));
		NavigateToAddTransactionCommand = new NavigateCommand<AddTransactionViewModel>(navigationStore, () => new AddTransactionViewModel(portfolioViewModel, navigationStore));
		NavigateToTransactionHistoryCommand = new NavigateCommand<TransactionHistoryViewModel>(navigationStore, () => new TransactionHistoryViewModel(portfolioViewModel, navigationStore));
		NavigateToDistributionsCommand = new NavigateCommand<DistributionsViewModel>(navigationStore, () => new DistributionsViewModel(portfolioViewModel));

		HistoricValuesLineGraph = new LineGraphViewModel();
	}

	public DashboardViewModel(ObservableCollection<HoldingViewModel> mostInfluential_holdingViewModels, NavigationStore navigationStore, CurrencyModel selectedCurrency)
	{
		this._mostInfluentialHoldings = mostInfluential_holdingViewModels;
		this.SelectedCurrency = selectedCurrency;
	}

}

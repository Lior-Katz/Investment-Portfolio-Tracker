using PortfolioTracker.Commands;
using PortfolioTracker.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PortfolioTracker.ViewModels;
public class DashboardViewModel : ViewModelBase
{
	private readonly ObservableCollection<HoldingViewModel> _mostInfluentialholdings;
	public ObservableCollection<HoldingViewModel> MostInfluentialHoldings => _mostInfluentialholdings;

	public ICommand NavigateToAllHoldingsCommand { get; }

	public ICommand NavigateToTransactionHistoryCommand { get; }

	public ICommand NavigateToAddTransactionCommand { get; }

	public ICommand NavigateToDistributionsCommand { get; }
	public CurrencyViewModel SelectedCurrency { get; } = new CurrencyViewModel();

	public DashboardViewModel(PortfolioViewModel portfolioViewModel, NavigationStore navigationStore)
	{
		this._mostInfluentialholdings = portfolioViewModel.MostInfluentialHoldings;

		NavigateToAllHoldingsCommand = new NavigateCommand<HoldingsListingViewModel>(navigationStore, () => new HoldingsListingViewModel());
		NavigateToAddTransactionCommand = new NavigateCommand<AddTransactionViewModel>(navigationStore, () => new AddTransactionViewModel(portfolioViewModel, navigationStore));
		NavigateToTransactionHistoryCommand = new NavigateCommand<TransactionHistoryViewModel>(navigationStore, () => new TransactionHistoryViewModel(portfolioViewModel, navigationStore));
		NavigateToDistributionsCommand = new NavigateCommand<DistributionsViewModel>(navigationStore, () => new DistributionsViewModel());
	}

	DashboardViewModel(ObservableCollection<HoldingViewModel> mostInfluential_holdingViewModels, NavigationStore navigationStore, CurrencyViewModel selectedCurrency)
	{
		this._mostInfluentialholdings = mostInfluential_holdingViewModels;
		this.SelectedCurrency = selectedCurrency;
	}

}

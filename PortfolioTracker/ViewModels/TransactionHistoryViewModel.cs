using System.Windows.Input;
using PortfolioTracker.Commands;
using PortfolioTracker.Stores;

namespace PortfolioTracker.ViewModels;

public class TransactionHistoryViewModel : ViewModelBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TransactionHistoryViewModel" /> class, with an ObservableCollection of
    ///     TradeViewModels.
    ///     <param name="holdingViewModels">An ObservableCollection of TradeViewModels to be shown on the view.</param>
    /// </summary>
    public TransactionHistoryViewModel(PortfolioViewModel portfolioViewModel, NavigationStore navigationStore)
    {
        PortfolioViewModel = portfolioViewModel;
        NavigateToAddTransactionCommand =
            new NavigateCommand<AddTransactionViewModel>(navigationStore,
                                                         () => new AddTransactionViewModel(portfolioViewModel,
                                                                  navigationStore));
    }

    public PortfolioViewModel PortfolioViewModel { get; }

    public ICommand NavigateToAddTransactionCommand { get; }
}

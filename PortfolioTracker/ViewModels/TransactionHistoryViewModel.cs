using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PortfolioTracker.Commands;
using PortfolioTracker.Stores;

namespace PortfolioTracker.ViewModels;

public class TransactionHistoryViewModel : ViewModelBase
{
    private readonly ObservableCollection<TradeViewModel> _transactions;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TransactionHistoryViewModel" /> class, with an ObservableCollection of
    ///     TradeViewModels.
    ///     <param name="holdingViewModels">An ObservableCollection of TradeViewModels to be shown on the view.</param>
    /// </summary>
    public TransactionHistoryViewModel(PortfolioViewModel portfolioViewModel, NavigationStore navigationStore)
    {
        _transactions = portfolioViewModel.Trades;

        NavigateToAddTransactionCommand =
            new NavigateCommand<AddTransactionViewModel>(navigationStore,
                                                         () => new AddTransactionViewModel(portfolioViewModel,
                                                                  navigationStore));
    }

    public ICommand NavigateToAddTransactionCommand { get; }
    public IEnumerable<TradeViewModel> Transactions => _transactions;
}

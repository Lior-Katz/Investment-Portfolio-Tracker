using System;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using PortfolioTracker.Models;
using PortfolioTracker.Stores;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Commands;

public class ConfirmAddTransactionCommand : NavigateCommand<TransactionHistoryViewModel>
{
    private readonly AddTransactionDialogViewModel _addTransactionDialogViewModel = new();

    /// <summary>
    ///     The view model that contains the input from the user through the AddTransactionView
    /// </summary>
    private readonly AddTransactionViewModel _addTransactionViewModel;

    /// <summary>
    ///     The portfolio to which the new transaction should be added
    /// </summary>
    private readonly PortfolioViewModel _portfolio;


    /// <summary>
    ///     Initializes a new instance of ConfirmAddTransactionCommand with the portfolio to add the transaction to,
    ///     and the the ViewModel that contains the transaction information.
    /// </summary>
    /// <param name="addTransactionViewModel">
    ///     The ViewModel that contains the information of the trade, through binding to user
    ///     input fields
    /// </param>
    /// <param name="portfolio">The portfolio that the trade should be added to</param>
    public ConfirmAddTransactionCommand(AddTransactionViewModel addTransactionViewModel,
                                        PortfolioViewModel portfolio,
                                        NavigationStore navigationStore)
        : base(navigationStore,
               () => App.AppHost.Services.GetRequiredService<TransactionHistoryViewModel>())
    {
        _addTransactionViewModel = addTransactionViewModel;
        _portfolio = portfolio;

        // subscribe to onPropertyChanged Event of AddTransactionViewModel for updating canExecute
        _addTransactionViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    /// <summary>
    ///     Checks whether the command should execute in the current state by validating user input.
    /// </summary>
    /// <param name="parameter">This parameter is not used, and can be set to null.</param>
    /// <returns></returns>
    public override bool CanExecute(object? parameter)
    {
        // additional validity checks must be updated in OnViewModelPropertyChanged
        var isNameValid = !string.IsNullOrEmpty(_addTransactionViewModel.Name);
        var isDateValid = _addTransactionViewModel.Date <= DateTime.Now;
        var isQuantityValid = _addTransactionViewModel.Quantity > 0;
        var isPriceValid = _addTransactionViewModel.Rate > 0;

        return isNameValid && isDateValid && isQuantityValid && isPriceValid && base.CanExecute(parameter);
    }

    /// <summary>
    ///     Executes the command by adding a new transaction that contains all the information from the user input to the
    ///     portfolio.
    /// </summary>
    /// <param name="parameter">This parameter is not used, and can be set to null.</param>
    public override void Execute(object? parameter)
    {
        
        var trade = new Trade(_addTransactionViewModel.Name,
                              _addTransactionViewModel.Ticker,
                              _addTransactionViewModel.IsBuyOrder,
                              DateOnly.FromDateTime(_addTransactionViewModel.Date),
                              _addTransactionViewModel.Quantity.ToString(),
                              _addTransactionViewModel.Rate.ToString(),
                              "0", "0",
                              // taxPaid.ToString(),
                              // commissionPaid.ToString(),
                              new CurrencyModel(_addTransactionViewModel.Currency));

        if (_addTransactionViewModel.IsBuyOrder)
        {
            ExecuteBuy(parameter, trade);
        }
        else
        {
            ExecuteSell(parameter);
        }

        _portfolio.AddTransaction(trade); // should be after executeBuy or executeSell because these methods change it
        base.Execute(parameter);
    }

    private void ExecuteSell(object? parameter)
    {
        
    }

    private void ExecuteBuy(object? parameter, Trade trade)
    {
        // Check if there is already a holding with a matching ticker symbol in the portfolio.
        var isHoldingExist = _portfolio.isHoldingExist(_addTransactionViewModel.Ticker);

        if (!isHoldingExist && !getAdditionalInfo())
        {
            return;
        }

        // var taxPaid = _addTransactionViewModel.TaxRate * _addTransactionViewModel.Rate *
        //               _addTransactionViewModel.Quantity;
        // var commissionPaid = _addTransactionViewModel.CommissionRate * _addTransactionViewModel.Rate *
        //                      _addTransactionViewModel.Quantity;


        if (!isHoldingExist)
        {
            addNewHolding(trade);
        }
    }

    private void addNewHolding(Trade trade)
    {
        var holding = new Holding(_addTransactionViewModel.Name,
                                  _addTransactionViewModel.Ticker,
                                  /*_addTransactionViewModel.Quantity*/ 0, // quantity is updated in AddTrade
                                  DateOnly.FromDateTime(_addTransactionViewModel.Date),
                                  // _addTransactionDialogViewModel.PayoutYield,
                                  // _addTransactionDialogViewModel.PayoutTax,
                                  // _addTransactionDialogViewModel.PayoutCommission,
                                  // _addTransactionDialogViewModel.PayoutPeriod,
                                  _addTransactionDialogViewModel.AssetType,
                                  _addTransactionDialogViewModel.Sector,
                                  _addTransactionDialogViewModel.Market);

        _portfolio.AddToHoldings(holding);
    }

    /// <summary>
    ///     Prompts the user to input additional info for holdings that do not appear in the portfolio yet.
    /// </summary>
    /// <returns></returns>
    private bool getAdditionalInfo()
    {
        // var dialog = new AddTransactionDialog
        //              {
        //                  DataContext = _addTransactionDialogViewModel
        //              };
        //
        // if (dialog.ShowDialog() == false)
        // {
        //     MessageBox.Show("Must fill details to add transaction.");
        //     return false;
        // }

        _addTransactionDialogViewModel.Market = "NYSE";
        _addTransactionDialogViewModel.Sector = "Technology";
        _addTransactionDialogViewModel.AssetType = "Stock";
        return true;
    }

    /// <summary>
    ///     Handles the property changes in AddTransactionViewModel by checking if they affect the value of CanExecute.
    /// </summary>
    /// <param name="sender">The object that triggered the event. Not used and can be set to null.</param>
    /// <param name="e">Event arguments containing information about the event. Not used and can be set to null.</param>
    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AddTransactionViewModel.Name) ||
            e.PropertyName == nameof(AddTransactionViewModel.Date) ||
            e.PropertyName == nameof(AddTransactionViewModel.Quantity) ||
            e.PropertyName == nameof(AddTransactionViewModel.Rate))
        {
            OnCanExecuteChanged();
        }
    }
}

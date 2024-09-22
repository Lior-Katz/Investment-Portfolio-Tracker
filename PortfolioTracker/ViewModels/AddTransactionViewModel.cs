using System;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using PortfolioTracker.Commands;
using PortfolioTracker.Stores;

/// <summary>
/// ViewModel for adding a new transaction, derived from the base ViewModel class.
/// </summary>
namespace PortfolioTracker.ViewModels;

public class AddTransactionViewModel : ViewModelBase
{
    // TODO: allow decimal input
    /// <summary>
    ///     Gets or sets the commission rate for the transaction.
    /// </summary>
    private decimal _commissionRate;

    /// <summary>
    ///     Gets or sets the currency of the transaction.
    /// </summary>
    private CurrencyModel _currency = new();

    /// <summary>
    ///     Gets or sets the date of the transaction, defaulting to the current date and time.
    /// </summary>
    private DateTime _date = DateTime.Now;

    /// <summary>
    ///     Gets or sets a boolean indicating whether the transaction is a buy order(true) or a sell order(false).
    /// </summary>
    private bool _isBuyOrder;

    /// <summary>
    ///     Gets or sets the name associated with the transaction.
    /// </summary>
    private string _name;

    /// <summary>
    ///     Gets or sets the quantity of the transaction.
    /// </summary>
    private decimal _quantity;

    /// <summary>
    ///     Gets or sets the rate of the transaction.
    /// </summary>
    private decimal _rate;

    // TODO: allow decimal input
    /// <summary>
    ///     Gets or sets the tax rate for the transaction.
    /// </summary>
    private decimal _taxRate;

    /// <summary>
    ///     3 or 4 letter ticker symbol
    /// </summary>
    private string _ticker;

    // TODO: Update documentation
    /// <summary>
    ///     Initializes a new instance of the <see cref="AddTransactionViewModel" /> class.
    /// </summary>
    /// <param name="portfolio">The portfolio to which the transaction will be added.</param>
    public AddTransactionViewModel(PortfolioViewModel portfolioViewModel, NavigationStore navigationStore)
    {
        ConfirmCommand = new ConfirmAddTransactionCommand(this, portfolioViewModel, navigationStore);
        CancelCommand =
            new NavigateCommand<DashboardViewModel>(navigationStore,
                                                    () => App.AppHost.Services
                                                             .GetRequiredService<DashboardViewModel>());
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public string Ticker
    {
        get => _ticker;
        set
        {
            _ticker = value;
            OnPropertyChanged(nameof(Ticker));
        }
    }

    public bool IsBuyOrder
    {
        get => _isBuyOrder;
        set
        {
            _isBuyOrder = value;
            OnPropertyChanged(nameof(IsBuyOrder));
        }
    }

    public decimal Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged(nameof(Quantity));
        }
    }

    public decimal Rate
    {
        get => _rate;
        set
        {
            _rate = value;
            OnPropertyChanged(nameof(Rate));
        }
    }

    public string Currency
    {
        get => _currency.ToString();
        set
        {
            _currency = new CurrencyModel(value);
            OnPropertyChanged(nameof(Currency));
        }
    }

    public DateTime Date
    {
        get => _date;
        set
        {
            _date = value;
            OnPropertyChanged(nameof(Date));
        }
    }

    public decimal CommissionRate
    {
        get => _commissionRate;
        set
        {
            _commissionRate = value;
            OnPropertyChanged(nameof(CommissionRate));
        }
    }

    public decimal TaxRate
    {
        get => _taxRate;
        set
        {
            _taxRate = value;
            OnPropertyChanged(nameof(TaxRate));
        }
    }

    /// <summary>
    ///     Gets the command for confirming and adding the transaction to the portfolio.
    /// </summary>
    public ICommand ConfirmCommand { get; }

    /// <summary>
    ///     Gets the command for canceling the transaction addition.
    /// </summary>
    public ICommand CancelCommand { get; }
}

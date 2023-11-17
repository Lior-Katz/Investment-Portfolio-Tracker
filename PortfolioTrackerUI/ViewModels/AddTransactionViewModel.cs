using PortfolioTracker.Commands;
using PortfolioTracker.Stores;
using System;
using System.Windows.Input;

/// <summary>
/// ViewModel for adding a new transaction, derived from the base ViewModel class.
/// </summary>
namespace PortfolioTracker.ViewModels
{

	public class AddTransactionViewModel : ViewModelBase
	{

		/// <summary>
		/// Gets or sets the name associated with the transaction.
		/// </summary>
		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		/// <summary>
		/// 3 or 4 letter ticker symbol
		/// </summary>
		private string _ticker;
		public string Ticker
		{
			get
			{
				return _ticker;
			}
			set
			{
				_ticker = value;
				OnPropertyChanged(nameof(Ticker));
			}
		}

		/// <summary>
		/// Gets or sets a boolean indicating whether the transaction is a buy order(true) or a sell order(false).
		/// </summary>
		private bool isBuyOrder;
		public bool IsBuyOrder
		{
			get
			{
				return isBuyOrder;
			}
			set
			{
				isBuyOrder = value;
				OnPropertyChanged(nameof(IsBuyOrder));
			}
		}

		/// <summary>
		/// Gets or sets the quantity of the transaction.
		/// </summary>
		private decimal _quantity;
		public decimal Quantity
		{
			get
			{
				return _quantity;
			}
			set
			{
				_quantity = value;
				OnPropertyChanged(nameof(Quantity));
			}
		}

		/// <summary>
		/// Gets or sets the rate of the transaction.
		/// </summary>
		private decimal _rate;
		public decimal Rate
		{
			get
			{
				return _rate;
			}
			set
			{
				_rate = value;
				OnPropertyChanged(nameof(Rate));
			}
		}

		/// <summary>
		/// Gets or sets the currency of the transaction.
		/// </summary>
		private CurrencyModel _currency = new CurrencyModel();
		public string Currency
		{
			get
			{
				return _currency?.ToString();
			}
			set
			{
				_currency = new CurrencyModel(value);
				OnPropertyChanged(nameof(Currency));
			}
		}

		/// <summary>
		/// Gets or sets the date of the transaction, defaulting to the current date and time.
		/// </summary>
		private DateTime _date = DateTime.Now;
		public DateTime Date
		{
			get
			{
				return _date;
			}
			set
			{
				_date = value;
				OnPropertyChanged(nameof(Date));
			}
		}

		// TODO: allow decimal input
		/// <summary>
		/// Gets or sets the commission rate for the transaction.
		/// </summary>
		private decimal _commissionRate;
		public decimal CommissionRate
		{
			get
			{
				return _commissionRate;
			}
			set
			{
				_commissionRate = value;
				OnPropertyChanged(nameof(CommissionRate));
			}
		}

		// TODO: allow decimal input
		/// <summary>
		/// Gets or sets the tax rate for the transaction.
		/// </summary>
		private decimal _taxRate;
		public decimal TaxRate
		{
			get
			{
				return _taxRate;
			}
			set
			{
				_taxRate = value;
				OnPropertyChanged(nameof(TaxRate));
			}
		}

		/// <summary>
		/// Gets the command for confirming and adding the transaction to the portfolio.
		/// </summary>
		public ICommand ConfirmCommand { get; }

		/// <summary>
		/// Gets the command for canceling the transaction addition.
		/// </summary>
		public ICommand CancelCommand { get; }

		// TODO: maybe change parameter to be PortfolioViewModel
		/// <summary>
		/// Initializes a new instance of the <see cref="AddTransactionViewModel"/> class.
		/// </summary>
		/// <param name="portfolio">The portfolio to which the transaction will be added.</param>
		public AddTransactionViewModel(PortfolioViewModel portfolioViewModel, NavigationStore navigationStore)
		{
			ConfirmCommand = new ConfirmAddTransactionCommand(this, portfolioViewModel, navigationStore);
			CancelCommand = new NavigateCommand<DashboardViewModel>(navigationStore, () => new DashboardViewModel(portfolioViewModel, navigationStore));
		}
	}
}

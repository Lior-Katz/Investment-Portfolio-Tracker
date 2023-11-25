using PortfolioTracker.Models;
using PortfolioTracker.Stores;
using PortfolioTracker.ViewModels;
using PortfolioTracker.Views.UserControls;
using System;
using System.ComponentModel;
using System.Windows;

namespace PortfolioTracker.Commands
{
	public class ConfirmAddTransactionCommand : NavigateCommand<TransactionHistoryViewModel>
	{
		/// <summary>
		/// The view model that contains the input from the user through the AddTransactionView
		/// </summary>
		private readonly AddTransactionViewModel _addTransactionViewModel;

		private readonly AddTransactionDialogViewModel _addTransactionDialogViewModel = new AddTransactionDialogViewModel();

		/// <summary>
		/// The portfolio to which the new transaction should be added
		/// </summary>
		private readonly PortfolioViewModel _portfolio;



		/// <summary>
		/// Initializes a new instance of ConfirmAddTransactionCommand with the porfolio to add the transaction to,
		/// and the the ViewModel that contains the transaction information.
		/// </summary>
		/// <param name="addTransactionViewModel">The ViewModel that contains the information of the trade, through binding to user input fields</param>
		/// <param name="portfolio">The portfolio that the trade should be added to</param>
		public ConfirmAddTransactionCommand(AddTransactionViewModel addTransactionViewModel, PortfolioViewModel portfolio, NavigationStore navigationStore) : base(navigationStore, () => new TransactionHistoryViewModel(portfolio, navigationStore))
		{
			this._addTransactionViewModel = addTransactionViewModel;
			this._portfolio = portfolio;

			// subscribe to onPropertyChanged Event of AddTransactionViewModel for updating canExecute
			_addTransactionViewModel.PropertyChanged += OnViewModelPropertyChanged;
		}

		/// <summary>
		/// Checks whether the command should execute in the current state by validating user input.
		/// </summary>
		/// <param name="parameter">This parameter is not used, and can be set to null.</param>
		/// <returns></returns>
		public override bool CanExecute(object? parameter)
		{
			// additional validity checks must be updated in OnViewModelPropertyChanged
			bool isNameValid = !string.IsNullOrEmpty(_addTransactionViewModel.Name);
			bool isDateValid = _addTransactionViewModel.Date <= DateTime.Now;
			bool isQuantityValid = _addTransactionViewModel.Quantity > 0;
			bool isPriceValid = _addTransactionViewModel.Rate > 0;

			return isNameValid && isDateValid && isQuantityValid && isPriceValid && base.CanExecute(parameter);
		}

		/// <summary>
		/// Executes the command by adding a new transaction that contains all the information from the user input to the portfolio.
		/// </summary>
		/// <param name="parameter">This parameter is not used, and can be set to null.</param>
		public override void Execute(object? parameter)
		{
			bool isHoldingExist = _portfolio.isHoldingExist(_addTransactionViewModel.Ticker);
			if (!checkFirstTransactionDetails(_addTransactionViewModel.Ticker, isHoldingExist))
				return;

			decimal taxPaid = _addTransactionViewModel.TaxRate * _addTransactionViewModel.Rate * _addTransactionViewModel.Quantity;
			decimal commissionPaid = _addTransactionViewModel.CommissionRate * _addTransactionViewModel.Rate * _addTransactionViewModel.Quantity;

			Trade trade = new Trade(_addTransactionViewModel.Name,
				_addTransactionViewModel.Ticker,
				 _addTransactionViewModel.OrderType,
				   DateOnly.FromDateTime(_addTransactionViewModel.Date),
					 _addTransactionViewModel.Quantity.ToString(),
					 _addTransactionViewModel.Rate.ToString(),
					 taxPaid.ToString(),
						commissionPaid.ToString(),
						   new CurrencyModel(_addTransactionViewModel.Currency));

			trade = _portfolio.AddTransaction(trade);

			if (isHoldingExist)
			{
				base.Execute(parameter);
				return;
			}


			Holding holding = new Holding(_addTransactionViewModel.Name,
					_addTransactionViewModel.Ticker,
					 _addTransactionViewModel.Quantity,
					 DateOnly.FromDateTime(_addTransactionViewModel.Date),
					 _addTransactionDialogViewModel.PayoutYield,
					 _addTransactionDialogViewModel.PayoutTax,
					 _addTransactionDialogViewModel.PayoutCommission,
					 _addTransactionDialogViewModel.PayoutPeriod,
					 _addTransactionDialogViewModel.AssetType,
					 _addTransactionDialogViewModel.Sector,
					  _addTransactionDialogViewModel.Market);

			holding.addTrade(trade);
			_portfolio.AddToHoldings(holding);

			base.Execute(parameter);
		}

		private bool checkFirstTransactionDetails(string ticker, bool isHoldingExist)
		{
			if (!isHoldingExist)
			{
				if (!getAdditionalInfo())
				{
					MessageBox.Show("Must fill details to add transaction.");
					return false;
				}
			}
			return true;
		}

		private bool getAdditionalInfo()
		{
			AddTransactionDialog dialog = new AddTransactionDialog()
			{
				DataContext = _addTransactionDialogViewModel
			};

			return dialog.ShowDialog() ?? false;
		}

		/// <summary>
		/// Handels the property changes in AddTransactionViewModel by checking if they affect the value of CanExecute.
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
}

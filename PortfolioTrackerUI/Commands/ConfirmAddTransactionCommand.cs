using PortfolioTracker.Models;
using PortfolioTracker.ViewModels;
using System;
using System.ComponentModel;

namespace PortfolioTracker.Commands
{
	public class ConfirmAddTransactionCommand : CommandsBase
	{
		/// <summary>
		/// The view model that contains the input from the user through the AddTransactionView
		/// </summary>
		private readonly AddTransactionViewModel _addTransactionViewModel;

		/// <summary>
		/// The portfolio to which the new transaction should be added
		/// </summary>
		private readonly Portfolio _portfolio;

		/// <summary>
		/// Initializes a new instance of ConfirmAddTransactionCommand with the porfolio to add the transaction to,
		/// and the the ViewModel that contains the transaction information.
		/// </summary>
		/// <param name="addTransactionViewModel">The ViewModel that contains the information of the trade, through binding to user input fields</param>
		/// <param name="portfolio">The portfolio that the trade should be added to</param>
		public ConfirmAddTransactionCommand(AddTransactionViewModel addTransactionViewModel, Portfolio portfolio)
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
			bool isDateValid = _addTransactionViewModel.Date >= DateTime.Now;
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
			decimal taxPaid = _addTransactionViewModel.TaxRate * _addTransactionViewModel.Rate * _addTransactionViewModel.Quantity;
			decimal commissionPaid = _addTransactionViewModel.CommissionRate * _addTransactionViewModel.Rate * _addTransactionViewModel.Quantity;

			Trade trade = new Trade(_addTransactionViewModel.Name,
				 _addTransactionViewModel.IsBuyOrder,
				   DateOnly.FromDateTime(_addTransactionViewModel.Date),
					 _addTransactionViewModel.Quantity.ToString(),
					 _addTransactionViewModel.Rate.ToString(),
					 taxPaid.ToString(),
						commissionPaid.ToString());

			_portfolio.addTrade(trade);
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

﻿using PortfolioTracker.Models;
using PortfolioTracker.Stores;
using PortfolioTracker.ViewModels;
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
			decimal taxPaid = _addTransactionViewModel.TaxRate * _addTransactionViewModel.Rate * _addTransactionViewModel.Quantity;
			decimal commissionPaid = _addTransactionViewModel.CommissionRate * _addTransactionViewModel.Rate * _addTransactionViewModel.Quantity;

			Trade trade = new Trade(_addTransactionViewModel.Name,
				_addTransactionViewModel.Ticker,
				 _addTransactionViewModel.IsBuyOrder,
				   DateOnly.FromDateTime(_addTransactionViewModel.Date),
					 _addTransactionViewModel.Quantity.ToString(),
					 _addTransactionViewModel.Rate.ToString(),
					 taxPaid.ToString(),
						commissionPaid.ToString(),
						   new CurrencyModel(_addTransactionViewModel.Currency));

			if (!_portfolio.AddTransaction(trade))
			{
				// TODO: recieve more information for first time buy
				MessageBox.Show("First time buy- more info required");

				Holding holding = new Holding(_addTransactionViewModel.Name,
					_addTransactionViewModel.Ticker,
					 _addTransactionViewModel.Quantity,
					 DateOnly.FromDateTime(_addTransactionViewModel.Date),
					 0,
					 0,
					 0,
					 0,
					 "",
					 "",
					  "");
				holding.addTrade(trade);
				_portfolio.AddToHoldings(holding);
			}

			base.Execute(parameter);
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

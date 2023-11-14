using PortfolioTracker.Commands;
using PortfolioTracker.Models;
using System;
using System.Windows.Input;

namespace PortfolioTracker.ViewModels
{
	public class AddTransactionViewModel : ViewModelBase
	{
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

		// TODO: bind to combobox
		// TODO: figure out data type
		private string _currency;
		public string Currency
		{
			get
			{
				return _currency;
			}
			set
			{
				_currency = value;
				OnPropertyChanged(nameof(Currency));
			}
		}

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


		public ICommand ConfirmCommand { get; }
		public ICommand CancelCommand { get; }

		public AddTransactionViewModel(Portfolio portfolio)
		{
			ConfirmCommand = new ConfirmAddTransactionCommand(this, portfolio);
			CancelCommand = new CancelAddTransactionCommands();
		}
	}
}

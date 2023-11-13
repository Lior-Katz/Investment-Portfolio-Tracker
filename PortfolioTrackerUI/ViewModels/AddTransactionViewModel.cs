using System;
using System.Windows.Input;

namespace PortfolioTracker.ViewModels
{
	internal class AddTransactionViewModel : ViewModelBase
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

		private DateOnly _date;
		public DateOnly Date
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

		private decimal _commission;
		public decimal Commission
		{
			get
			{
				return _commission;
			}
			set
			{
				_commission = value;
				OnPropertyChanged(nameof(Commission));
			}
		}

		private decimal _tax;
		public decimal Tax
		{
			get
			{
				return _tax;
			}
			set
			{
				_tax = value;
				OnPropertyChanged(nameof(Tax));
			}
		}

		public ICommand ConfirmCommand { get; }
		public ICommand CancelCommand { get; }

        public AddTransactionViewModel()
        {
            
        }
    }
}

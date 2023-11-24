namespace PortfolioTracker.ViewModels
{
	class AddTransactionDialogViewModel : ViewModelBase
	{
		//private string _ticker = "";
		//public string Ticker
		//{
		//	get
		//	{
		//		return _ticker;
		//	}
		//	set
		//	{
		//		_ticker = value;
		//		OnPropertyChanged(nameof(Ticker));
		//	}
		//}

		private decimal _payoutYield;
		public decimal PayoutYield
		{
			get
			{
				return _payoutYield;
			}
			set
			{
				_payoutYield = value;
				OnPropertyChanged(nameof(PayoutYield));
			}
		}

		private decimal _payoutTax;
		public decimal PayoutTax
		{
			get
			{
				return _payoutTax;
			}
			set
			{
				_payoutTax = value;
				OnPropertyChanged(nameof(PayoutTax));
			}
		}

		private decimal _payoutCommission;
		public decimal PayoutCommission
		{
			get
			{
				return _payoutCommission;
			}
			set
			{
				_payoutCommission = value;
				OnPropertyChanged(nameof(PayoutCommission));
			}
		}

		// TODO: figure out binding
		private int _payoutPeriod;
		public int PayoutPeriod
		{
			get
			{
				return _payoutPeriod;
			}
			set
			{
				_payoutPeriod = value;
				OnPropertyChanged(nameof(PayoutPeriod));
			}
		}

		private string _assetType;
		public string AssetType
		{
			get
			{
				return _assetType;
			}
			set
			{
				_assetType = value;
				OnPropertyChanged(nameof(AssetType));
			}
		}

		private string _sector = "";
		public string Sector
		{
			get
			{
				return _sector;
			}
			set
			{
				_sector = value;
				OnPropertyChanged(nameof(Sector));
			}
		}

		private string _market = "";
		public string Market
		{
			get
			{
				return _market;
			}
			set
			{
				_market = value;
				OnPropertyChanged(nameof(Market));
			}
		}

		//public Payout Payout => new Payout(PayoutYield, PayoutTax, PayoutCommission, PayoutPeriod, DateOnly.FromDateTime(DateTime.Today));


	}
}

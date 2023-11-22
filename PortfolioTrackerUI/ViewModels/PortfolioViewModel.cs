using PortfolioTracker.Models;
using System.Collections.ObjectModel;

namespace PortfolioTracker.ViewModels;
public class PortfolioViewModel : ViewModelBase
{
	private readonly Portfolio _portfolio;

	/// <summary>
	/// The unique identifier of the portfolio.
	/// </summary>
	public int Id => _portfolio.Id;
	/// <summary>
	/// The name of the portfolio.
	/// </summary>
	public string Name => _portfolio.Name;

	public decimal Value => _portfolio.Value;

	public decimal DailyChange => _portfolio.DailyChange;

	public decimal DailyChangePercentage => _portfolio.DailyChangePercentage;

	/// <summary>
	/// A list of all assets currently held in the portfolio.
	/// </summary>
	public ObservableCollection<HoldingViewModel> Holdings
	{
		get
		{
			ObservableCollection<HoldingViewModel> result = new ObservableCollection<HoldingViewModel>();
			foreach (Holding holding in _portfolio.Holdings)
			{
				result.Add(new HoldingViewModel(holding));
			}
			return result;
		}
	}
	/// <summary>
	/// A record of all trades recorded in this portfolio.
	/// </summary>
	public ObservableCollection<TradeViewModel> Trades
	{
		get
		{
			ObservableCollection<TradeViewModel> result = new ObservableCollection<TradeViewModel>();
			foreach (Trade trade in _portfolio.Trades)
			{
				result.Add(new TradeViewModel(trade));
			}
			return result;
		}
	}

	public ObservableCollection<HoldingViewModel> MostInfluentialHoldings
	{
		get
		{
			ObservableCollection<HoldingViewModel> result = new ObservableCollection<HoldingViewModel>();
			foreach (Holding holding in _portfolio.MostInfluentialHoldings)
			{
				result.Add(new HoldingViewModel(holding));
			}
			return result;
		}
	}

	public PortfolioViewModel(Portfolio portfolio)
	{
		this._portfolio = portfolio;

		//_portfolio.PropertyChanged += OnPortfolioChanged;
		//Holdings.CollectionChanged += OnCollectionChanged;
		//Trades.CollectionChanged += OnCollectionChanged;
	}

	public bool AddTransaction(Trade trade)
	{
		bool res = _portfolio.AddTrade(trade);
		onPortfolioChanged(nameof(Trades));
		return res;
	}
	public void RemoveTransaction(Trade trade) => _portfolio.RemoveTrade(trade);

	public void AddToHoldings(Holding holding)
	{
		_portfolio.AddToHoldings(holding);
		onPortfolioChanged(nameof(Holdings));
	}
	//private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
	//{
	//	OnPropertyChanged(nameof(Value));
	//}
	//private void OnPortfolioChanged(object? sender, PropertyChangedEventArgs e)
	//{
	//	OnPropertyChanged(nameof(Value));
	//}

	private void onPortfolioChanged(string propertyName)
	{
		OnPropertyChanged(nameof(Value));
		OnPropertyChanged(nameof(DailyChangePercentage));
		OnPropertyChanged(nameof(DailyChange));
	}

}

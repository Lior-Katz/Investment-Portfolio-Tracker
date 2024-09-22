using System;
using System.Collections.Generic;
using PortfolioTracker.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace PortfolioTracker.ViewModels;
public class PortfolioViewModel : ViewModelBase
{
	private readonly Portfolio _portfolio;

	/// <summary>
	/// The unique identifier of the portfolio.
	/// </summary>
	public int Id
	{
		get => _portfolio.Id;
		set => _portfolio.Id = value;
	}
	/// <summary>
	/// The name of the portfolio.
	/// </summary>
	public string Name => _portfolio.Name;
	
	public DateTime createdDate => _portfolio.CreatedDate;

	public decimal Value => _portfolio.Value;

	public decimal DailyChange => _portfolio.DailyChange;

	public decimal DailyChangePercentage => _portfolio.DailyChangePercentage;

	/// <summary>
	/// A list of all assets currently held in the portfolio.
	/// </summary>
	public ObservableCollection<HoldingViewModel> Holdings
	{
		get => new ObservableCollection<HoldingViewModel>(_portfolio.Holdings.Select(holding => new HoldingViewModel(holding)).ToList());
		set
		{
			_portfolio.Holdings = value.Select(holdingViewModel => holdingViewModel.ToHolding()).ToList();
		}
	}
	/// <summary>
	/// A record of all trades recorded in this portfolio.
	/// </summary>
	public ObservableCollection<TradeViewModel> Trades
	{
		get => new ObservableCollection<TradeViewModel>(_portfolio.Trades.Select(trade => new TradeViewModel(trade)).ToList());
		set
		{
			_portfolio.Trades = value.Select(tradeViewModel => tradeViewModel.ToTrade()).ToList();
		}
	}
	
	public List<KeyValuePair<DateTime, decimal>> HistoricalValue
	{
		get => _portfolio.HistoricalValue;
		set
		{
			_portfolio.HistoricalValue = value.ToList();
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

	public Trade AddTransaction(Trade trade)
	{
		Trade res = _portfolio.AddTrade(trade);
		onPortfolioChanged(nameof(Trades));
		return res;
	}
	public void RemoveTransaction(Trade trade) => _portfolio.RemoveTrade(trade);

	public void AddToHoldings(Holding holding)
	{
		_portfolio.AddToHoldings(holding);
		onPortfolioChanged(nameof(Holdings));
	}

	public decimal GetPercentageOfPortfolio(int holdingId) => _portfolio.GetPercentageOfPortfolio(holdingId);

	public bool isHoldingExist(string ticker) => _portfolio.isHoldingExist(ticker);

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

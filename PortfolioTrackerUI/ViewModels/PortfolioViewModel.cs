﻿using PortfolioTracker.Models;
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
	}

	public bool AddTransaction(Trade trade) => _portfolio.AddTrade(trade);

	public void RemoveTransaction(Trade trade) => _portfolio.RemoveTrade(trade);

	public void AddToHoldings(Holding holding) => _portfolio.AddToHoldings(holding);
}

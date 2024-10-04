using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PortfolioTracker.Models;

namespace PortfolioTracker.ViewModels;

public class PortfolioViewModel : ViewModelBase
{
    private readonly Portfolio _portfolio;

    public PortfolioViewModel(Portfolio portfolio)
    {
        _portfolio = portfolio;

        //_portfolio.PropertyChanged += OnPortfolioChanged;
        //Holdings.CollectionChanged += OnCollectionChanged;
        //Trades.CollectionChanged += OnCollectionChanged;
    }

    /// <summary>
    ///     The unique identifier of the portfolio.
    /// </summary>
    public int Id
    {
        get => _portfolio.Id;
        set => _portfolio.Id = value;
    }

    /// <summary>
    ///     The name of the portfolio.
    /// </summary>
    public string Name => _portfolio.Name;

    public DateTime createdDate => _portfolio.CreatedDate;

    public decimal Value => _portfolio.Value;

    public decimal DailyChange => _portfolio.DailyChange;

    public decimal DailyChangePercentage => _portfolio.DailyChangePercentage;

    /// <summary>
    ///     A list of all assets currently held in the portfolio.
    /// </summary>
    public ObservableCollection<HoldingViewModel> Holdings
    {
        get => new(_portfolio.Holdings.Select(holding => new HoldingViewModel(holding)).ToList());
        set { _portfolio.Holdings = value.Select(holdingViewModel => holdingViewModel.ToHolding()).ToList(); }
    }

    /// <summary>
    ///     A record of all trades recorded in this portfolio.
    /// </summary>
    public ObservableCollection<TradeViewModel> Trades
    {
        get => new(_portfolio.Trades.Select(trade => new TradeViewModel(trade)).ToList());
        set { _portfolio.Trades = value.Select(tradeViewModel => tradeViewModel.ToTrade()).ToList(); }
    }

    public List<KeyValuePair<DateTime, decimal>> HistoricalValue
    {
        get => _portfolio.HistoricalValue;
        set => _portfolio.HistoricalValue = value.ToList();
    }

    public ObservableCollection<HoldingViewModel> MostInfluentialHoldings
    {
        get
        {
            var result = new ObservableCollection<HoldingViewModel>();
            foreach (var holding in _portfolio.MostInfluentialHoldings)
            {
                result.Add(new HoldingViewModel(holding));
            }

            return result;
        }
    }

    public Trade AddTransaction(Trade trade)
    {
        var res = _portfolio.AddTrade(trade);
        onPortfolioChanged(nameof(Trades));
        return res;
    }

    public void RemoveTransaction(Trade trade)
    {
        _portfolio.RemoveTrade(trade);
    }

    public void AddToHoldings(Holding holding)
    {
        _portfolio.AddToHoldings(holding);
        onPortfolioChanged(nameof(Holdings));
    }

    public decimal GetPercentageOfPortfolio(int holdingId)
    {
        return _portfolio.GetPercentageOfPortfolio(holdingId);
    }

    public bool isHoldingExist(string ticker)
    {
        return _portfolio.isHoldingExist(ticker);
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
        OnPropertyChanged(nameof(MostInfluentialHoldings));
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Fynance;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Services;

public class FinancialDataService : IFinancialDataService
{
    // TODO: handle exceptions for invalid ticker
    public decimal GetLastPrice(string ticker)
    {
        var result = Ticker.Build(ticker)
                           .SetPeriod(Period.OneDay)
                           .SetInterval(Interval.OneDay)
                           .Get();
        return result.Quotes[0].Close;
    }

    public IEnumerable<decimal> GetLastPrice(IEnumerable<string> tickers)
    {
        foreach (var ticker in tickers) yield return GetLastPrice(ticker);
    }

    public decimal GetDailyChange(string ticker)
    {
        var result = Ticker.Build(ticker)
                           .SetPeriod(Period.OneDay)
                           .SetInterval(Interval.OneDay)
                           .Get();

        return result.Quotes[0].Close - result.Quotes[0].Open;
    }

    public IEnumerable<decimal> GetDailyChange(IEnumerable<string> tickers)
    {
        foreach (var ticker in tickers) yield return GetDailyChange(ticker);
    }

    public IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(string ticker, DateTime startDate)
    {
        var result = Ticker.Build(ticker)
                           .SetStartDate(startDate)
                           .SetInterval(Interval.OneDay)
                           .Get();

        foreach (var quote in result.Quotes)
            if (typeof(TDate) == typeof(DateTime))
                yield return new KeyValuePair<TDate, decimal>((TDate)(object)quote.Period, quote.Close);
            else if (typeof(TDate) == typeof(DateOnly))
                yield return new KeyValuePair<TDate, decimal>((TDate)(object)DateOnly.FromDateTime(quote.Period),
                                                              quote.Open);
            else
                throw new ArgumentException("TDate must be " + typeof(DateOnly) + " or " + typeof(DateTime) +
                                            ", cannot be " + typeof(TDate));
    }

    public IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(string ticker, DateOnly startDate)
    {
        return GetHistoricalValue<TDate>(ticker, startDate.ToDateTime(new TimeOnly(0)));
    }

    public IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(string ticker, TimeSpan interval)
    {
        return GetHistoricalValue<TDate>(ticker, DateTime.Now - interval);
    }

    public decimal getValueOnDate(string ticker, DateTime date)
    {
        var result = Ticker.Build(ticker)
                           .SetStartDate(date)
                           .SetFinishDate(date.AddDays(1))
                           .SetInterval(Interval.OneDay)
                           .Get();
        return result.Quotes.Length > 0 ? result.Quotes[0].Close : result.ChartPreviousClose.Value;
    }

    public void CompleteHistory(PortfolioViewModel portfolioViewModel)
    {
        IEnumerable<KeyValuePair<DateTime, decimal>> historicalValue = portfolioViewModel.HistoricalValue;
        var lastUpdated = historicalValue.Count() > 0
                              ? historicalValue.Last().Key
                              : portfolioViewModel.createdDate.AddDays(-1);

        for (var date = lastUpdated.AddDays(1); date <= DateTime.Today; date = date.AddDays(1))
        {
            decimal value = 0;
            foreach (var (ticker, quantity) in
                     portfolioViewModel.Holdings.Select(holding => (holding.Ticker, holding.Quantity)))
                value += getValueOnDate(ticker, date) * quantity;
            portfolioViewModel.HistoricalValue.Add(new KeyValuePair<DateTime, decimal>(date, value));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Services;

public class MockFinancialDataService : IFinancialDataService
{
    // Mock all of the interface methods
    public decimal GetLastPrice(string ticker)
    {
        return 100;
    }

    public IEnumerable<decimal> GetLastPrice(IEnumerable<string> tickers)
    {
        foreach (var ticker in tickers)
        {
            yield return 100;
        }
    }

    public decimal GetDailyChange(string ticker)
    {
        return 10;
    }

    public IEnumerable<decimal> GetDailyChange(IEnumerable<string> tickers)
    {
        foreach (var ticker in tickers)
        {
            yield return 10;
        }
    }

    public IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(string ticker, DateTime startDate)
    {
        for (var i = 0; i < 10; i++)
        {
            yield return new KeyValuePair<TDate, decimal>((TDate)(object)startDate.AddDays(i), 100);
        }
    }

    public IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(string ticker, TimeSpan timeSpan)
    {
        var random = new Random();
        for (var i = 0; i < timeSpan.Days; i++)
        {
            yield return new KeyValuePair<TDate, decimal>((TDate)(object)DateTime.Now.AddDays(i - timeSpan.Days),
                                                          random.Next(0, 110));
        }
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
            foreach (var ticker in portfolioViewModel.Holdings.Select(holding => holding.Ticker))
            {
                value += getValueOnDate(ticker, date);
            }
        }
    }

    public decimal getValueOnDate(string ticker, DateTime date)
    {
        var random = new Random();
        return random.Next(0, 110);
    }

    public IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(string ticker, DateOnly startDate)
    {
        return GetHistoricalValue<TDate>(ticker, startDate.ToDateTime(new TimeOnly(0)));
    }

    public IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(
        string ticker, DateTime startDate, TimeSpan timeSpan)
    {
        for (var i = 0; i < timeSpan.Days; i++)
        {
            yield return new KeyValuePair<TDate, decimal>((TDate)(object)startDate.AddDays(i), 100);
        }
    }

    public IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(
        string ticker, TimeSpan timeSpan, int interval)
    {
        for (var i = 0; i < timeSpan.Days; i += interval)
        {
            yield return new KeyValuePair<TDate, decimal>((TDate)(object)DateTime.Now.AddDays(-i), 100);
        }
    }

    public IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(
        string ticker, DateTime startDate, TimeSpan timeSpan, int interval)
    {
        for (var i = 0; i < timeSpan.Days; i += interval)
        {
            yield return new KeyValuePair<TDate, decimal>((TDate)(object)startDate.AddDays(i), 100);
        }
    }
}

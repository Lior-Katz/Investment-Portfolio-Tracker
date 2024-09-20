using System;
using System.Collections.Generic;

namespace PortfolioTracker.Services;

public interface IFinancialDataService
{
    decimal GetLastPrice(string ticker);
    IEnumerable<decimal> GetLastPrice(IEnumerable<string> tickers);
    decimal GetDailyChange(string ticker);
    IEnumerable<decimal> GetDailyChange(IEnumerable<string> tickers);
    IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(string ticker, DateTime startDate);
    IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(string ticker, DateOnly startDate);
    IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(string ticker, TimeSpan interval);
}
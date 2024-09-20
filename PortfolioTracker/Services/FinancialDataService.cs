using Fynance;
using Fynance.Result;
using System;
using System.Collections.Generic;

namespace PortfolioTracker.Services
{
	public class FinancialDataService : IFinancialDataService
	{
		// TODO: handle exceptions for invalid ticker
		public decimal GetLastPrice(string ticker)
		{
			FyResult result = Ticker.Build(ticker)
					.SetPeriod(Period.OneDay)
					.SetInterval(Interval.OneDay)
					.Get();
			return result.Quotes[0].Close;
		}
		public IEnumerable<decimal> GetLastPrice(IEnumerable<string> tickers)
		{
			foreach (var ticker in tickers)
			{
				yield return GetLastPrice(ticker);
			}
		}

		public decimal GetDailyChange(string ticker)
		{
			FyResult result = Ticker.Build(ticker)
			.SetPeriod(Period.OneDay)
			.SetInterval(Interval.OneDay)
			.Get();

			return result.Quotes[0].Close - result.Quotes[0].Open;
		}

		public IEnumerable<decimal> GetDailyChange(IEnumerable<string> tickers)
		{
			foreach (var ticker in tickers)
			{
				yield return GetDailyChange(ticker);
			}
		}

		public IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(string ticker, DateTime startDate)
		{
			FyResult result = Ticker.Build(ticker)
			.SetStartDate(startDate)
			.SetInterval(Interval.OneDay)
			.Get();

			foreach (var quote in result.Quotes)
			{
				if (typeof(TDate) == typeof(DateTime))
				{
					yield return new KeyValuePair<TDate, decimal>((TDate) (object) quote.Period, quote.Close);
				}
				else if (typeof(TDate) == typeof(DateOnly))
				{
					yield return new KeyValuePair<TDate, decimal>((TDate) (object) DateOnly.FromDateTime(quote.Period), quote.Open);
				}
				else
				{
					throw new ArgumentException("TDate must be " + typeof(DateOnly) + " or " + typeof(DateTime) + ", cannot be " + typeof(TDate));
				}
			}
		}

		public IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(string ticker, DateOnly startDate)
		{
			return GetHistoricalValue<TDate>(ticker, startDate.ToDateTime(new TimeOnly(0)));
		}

		public IEnumerable<KeyValuePair<TDate, decimal>> GetHistoricalValue<TDate>(string ticker, TimeSpan interval)
		{
			return GetHistoricalValue<TDate>(ticker, DateTime.Now - interval);
		}


	}
}

using Fynance;
using Fynance.Result;
using System.Collections.Generic;

namespace PortfolioTracker.Services
{
	public class FinancialDataService
	{
		// TODO: handle exceptions for invalid ticker
		public static decimal GetLastPrice(string ticker)
		{
			FyResult result = Ticker.Build(ticker)
					.SetPeriod(Period.OneDay)
					.SetInterval(Interval.OneDay)
					.Get();
			return result.Quotes[0].Close;
		}
		public static IEnumerable<decimal> GetLastPrice(IEnumerable<string> tickers)
		{
			foreach (var ticker in tickers)
			{
				yield return GetLastPrice(ticker);
			}
		}

		public static decimal GetDailyChange(string ticker)
		{
			FyResult result = Ticker.Build(ticker)
			.SetPeriod(Period.OneDay)
			.SetInterval(Interval.OneDay)
			.Get();

			return result.Quotes[0].Close - result.Quotes[0].Open;
		}

		public static IEnumerable<decimal> GetDailyChange(IEnumerable<string> tickers)
		{
			foreach (var ticker in tickers)
			{
				yield return GetDailyChange(ticker);
			}
		}
	}
}

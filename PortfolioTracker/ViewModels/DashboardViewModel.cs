﻿using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using PortfolioTracker.Commands;
using PortfolioTracker.Services;
using PortfolioTracker.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PortfolioTracker.ViewModels;
public class DashboardViewModel : ViewModelBase
{
	private readonly ObservableCollection<HoldingViewModel> _mostInfluentialHoldings;
	public ObservableCollection<HoldingViewModel> MostInfluentialHoldings => _mostInfluentialHoldings;

	public ICommand NavigateToAllHoldingsCommand { get; }

	public ICommand NavigateToTransactionHistoryCommand { get; }

	public ICommand NavigateToAddTransactionCommand { get; }

	public ICommand NavigateToDistributionsCommand { get; }
	public CurrencyModel SelectedCurrency { get; }

	public LineGraphViewModel HistoricValuesLineGraph { get; }

	private TimeSpan historicalDataTimeSpan { get; set; } = new TimeSpan(40, 0, 0, 0);
	private TimeSpan graphTimeSpan { get; set; } = new TimeSpan(0);

	public DashboardViewModel(PortfolioViewModel portfolioViewModel, NavigationStore navigationStore)
	{
		this._mostInfluentialHoldings = portfolioViewModel.MostInfluentialHoldings;

		NavigateToAllHoldingsCommand = new NavigateCommand<HoldingsListingViewModel>(navigationStore, () => new HoldingsListingViewModel(portfolioViewModel));
		NavigateToAddTransactionCommand = new NavigateCommand<AddTransactionViewModel>(navigationStore, () => new AddTransactionViewModel(portfolioViewModel, navigationStore));
		NavigateToTransactionHistoryCommand = new NavigateCommand<TransactionHistoryViewModel>(navigationStore, () => new TransactionHistoryViewModel(portfolioViewModel, navigationStore));
		NavigateToDistributionsCommand = new NavigateCommand<DistributionsViewModel>(navigationStore, () => new DistributionsViewModel(portfolioViewModel));

		HistoricValuesLineGraph = GetHistoricalValuesLineGraph(portfolioViewModel);
		AddComparisonLineGraph(new string[] { "VOO", "QQQ" });

	}

	private void AddComparisonLineGraph(string[] comparisons)
	{
		List<KeyValuePair<string, List<decimal>>> comparisonValues = new List<KeyValuePair<string, List<decimal>>>();

		foreach (string ticker in comparisons)
		{
			List<KeyValuePair<DateTime, decimal>> rawData = FinancialDataService.GetHistoricalValue<DateTime>(ticker, graphTimeSpan).ToList();
			KeyValuePair<string, List<decimal>> dataWithEmptyDatesFilled = new KeyValuePair<string, List<decimal>>(ticker, new List<decimal>());
			for (int days = graphTimeSpan.Days; days >= 0; --days)
			{
				DateTime date = DateTime.Now - new TimeSpan(days, 0, 0, 0);
				dataWithEmptyDatesFilled.Value.Add(getLastValueBeforeDate(rawData, date));
			}
			comparisonValues.Add(dataWithEmptyDatesFilled);
		}
		foreach (KeyValuePair<string, List<decimal>> comparison in comparisonValues)
		{
			HistoricValuesLineGraph.Series = HistoricValuesLineGraph.Series.Append(new LineSeries<decimal>
			{
				Name = comparison.Key,
				Values = comparison.Value,
				YToolTipLabelFormatter = (chartPoint) => $"{Math.Round(chartPoint.Coordinate.PrimaryValue, 2)}",
				GeometrySize = 0
			}).ToArray();
		}

		HistoricValuesLineGraph.LegendPosition = LegendPosition.Top;
	}

	private LineGraphViewModel GetHistoricalValuesLineGraph(PortfolioViewModel portfolioViewModel)
	{
		List<KeyValuePair<DateTime, decimal>> dateValuePairs = GetDateValueList(portfolioViewModel);

		return new LineGraphViewModel(new ISeries[]
		{
			new LineSeries<decimal>{
				Name = portfolioViewModel.Name,
				Values = dateValuePairs.ConvertAll(x => x.Value),
				YToolTipLabelFormatter = (chartPoint) => $"{Math.Round(chartPoint.Coordinate.PrimaryValue, 2)}",
				GeometrySize = 0
			}
		},
		new List<Axis>
		{
			new Axis{
				Labels = dateValuePairs.ConvertAll(x => x.Key.ToString("d"))
			}
		},
		ZoomAndPanMode.X);
	}

	private List<KeyValuePair<DateTime, decimal>> GetDateValueList(PortfolioViewModel portfolioViewModel)
	{
		List<List<KeyValuePair<DateTime, decimal>>> AllSecuritiesData = new List<List<KeyValuePair<DateTime, decimal>>>();

		List<string> tickers = portfolioViewModel.Holdings.Select(holding => holding.Ticker).ToList();

		foreach (string ticker in tickers)
		{
			DateTime acquisitionDate = portfolioViewModel.Holdings.ToList().Find(holding => holding.Ticker == ticker).AcquisitionDate.ToDateTime(TimeOnly.MinValue);

			TimeSpan timeSpan = TimeSpan.FromTicks(Math.Min(historicalDataTimeSpan.Ticks, (DateTime.Now - acquisitionDate).Ticks));
			graphTimeSpan = TimeSpan.FromTicks(Math.Max(graphTimeSpan.Ticks, timeSpan.Ticks));

			// create a list of the value of the security on each date
			List<KeyValuePair<DateTime, decimal>> historicalSecurityValues = FinancialDataService.GetHistoricalValue<DateTime>(ticker, timeSpan).ToList();

			AllSecuritiesData.Add(historicalSecurityValues);
		}

		List<KeyValuePair<DateTime, decimal>> dateValuePairs = new List<KeyValuePair<DateTime, decimal>>(); // a list of the total value on each date
		for (int days = graphTimeSpan.Days; days >= 0; --days)
		{
			DateTime date = DateTime.Now - new TimeSpan(days, 0, 0, 0);


			// a list of the dataWithEmptyDatesFilled of every security on a specific date
			// for every security in AllSecuritiesData, find the pair corresponding to the specific date, and get its value.
			List<decimal> valuesOnDate = AllSecuritiesData.ConvertAll(securityData => getLastValueBeforeDate(securityData, date));

			// sum over every value on this specific date.
			dateValuePairs.Add(new KeyValuePair<DateTime, decimal>(date, valuesOnDate.Sum()));

		}
		return dateValuePairs;
	}

	private decimal getLastValueBeforeDate(List<KeyValuePair<DateTime, decimal>> securityData, DateTime date)
	{
		if (date.Date < securityData.First().Key.Date)
		{
			return 0;
		}
		KeyValuePair<DateTime, decimal> valueOnDate = securityData.Find(pair => pair.Key.Date == date.Date);

		// if valueOnDate is a default pair, no value is available for this date, so use the value from the day before.
		return valueOnDate.Equals(default(KeyValuePair<DateTime, decimal>)) ? getLastValueBeforeDate(securityData, date.AddDays(-1)) : valueOnDate.Value;
	}

	public DashboardViewModel(ObservableCollection<HoldingViewModel> mostInfluential_holdingViewModels, NavigationStore navigationStore, CurrencyModel selectedCurrency)
	{
		this._mostInfluentialHoldings = mostInfluential_holdingViewModels;
		this.SelectedCurrency = selectedCurrency;
	}

}
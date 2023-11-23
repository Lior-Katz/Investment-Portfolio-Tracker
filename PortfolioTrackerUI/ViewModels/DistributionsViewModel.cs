using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PortfolioTracker.ViewModels;
public class DistributionsViewModel : ViewModelBase
{
	//private readonly ObservableCollection<HoldingViewModel> _holdings;
	//public ObservableCollection<HoldingViewModel> Holdings
	//{
	//	get => _holdings;
	//}

	private readonly PieChartViewModel _assetTypePieChart;
	public PieChartViewModel AssetTypePieChart
	{
		get => _assetTypePieChart;
	}

	private readonly PieChartViewModel _sectorPieChart;
	public PieChartViewModel SectorPieChart
	{
		get => _sectorPieChart;
	}

	private readonly PieChartViewModel _marketPieChart;
	public PieChartViewModel MarketPieChart
	{
		get => _marketPieChart;
	}

	public DistributionsViewModel(PortfolioViewModel portfolioViewModel)
	{
		//this._holdings = portfolioViewModel.Holdings;

		Dictionary<string, decimal> assetTypeDict = new Dictionary<string, decimal>();
		Dictionary<string, decimal> marketDict = new Dictionary<string, decimal>();
		Dictionary<string, decimal> sectorDict = new Dictionary<string, decimal>();

		foreach (HoldingViewModel holdingViewModel in portfolioViewModel.Holdings)
		{
			decimal percentage = portfolioViewModel.GetPercentageOfPortfolio(holdingViewModel.Id);
			if (assetTypeDict.ContainsKey(holdingViewModel.Type))
			{
				assetTypeDict[holdingViewModel.Type] += percentage;
			}
			else
			{
				assetTypeDict.Add(holdingViewModel.Type, percentage);
			}

			if (marketDict.ContainsKey(holdingViewModel.Market))
			{
				marketDict[holdingViewModel.Market] += percentage;
			}
			else
			{
				marketDict.Add(holdingViewModel.Market, percentage);
			}

			if (sectorDict.ContainsKey(holdingViewModel.Sector))
			{
				sectorDict[holdingViewModel.Sector] += percentage;
			}
			else
			{
				sectorDict.Add(holdingViewModel.Sector, percentage);
			}
		}

		//List<PieSeries<decimal> series = new ISeries();
		//ObservableCollection<PieSeries<decimal>> arr = assetTypeDict.Select(pair => new PieSeries<decimal>
		//{
		//	Values = new[] {pair.Value},
		//	Name = pair.Key
		//}).ToList();
		//List<PieSeries<decimal>> parr = assetTypeDict.Select(pair => new PieSeries<decimal>
		//{
		//	Values = new[] { pair.Value },
		//	Name = pair.Key
		//}).ToList();
		//ObservableCollection<PieSeries<decimal>> arr = new ObservableCollection<PieSeries<decimal>>(parr);

		_assetTypePieChart = new PieChartViewModel(new ObservableCollection<PieSeries<decimal>>(assetTypeDict.Select(pair => new PieSeries<decimal>
		{
			Values = new[] { pair.Value },
			Name = pair.Key
		})),
		new LabelVisual
		{
			Text = "Asset Type",
			TextSize = 25,
			Padding = new LiveChartsCore.Drawing.Padding(15),
			Paint = new SolidColorPaint(SKColors.DarkSlateGray)
		});


		_sectorPieChart = new PieChartViewModel(new ObservableCollection<PieSeries<decimal>>(sectorDict.Select(pair => new PieSeries<decimal>
		{
			Values = new[] { pair.Value },
			Name = pair.Key
		})),
		 new LabelVisual
		 {
			 Text = "Sector",
			 TextSize = 25,
			 Padding = new LiveChartsCore.Drawing.Padding(15),
			 Paint = new SolidColorPaint(SKColors.DarkSlateGray)
		 });

		_marketPieChart = new PieChartViewModel(new ObservableCollection<PieSeries<decimal>>(marketDict.Select(pair => new PieSeries<decimal>
		{
			Values = new[] { pair.Value },
			Name = pair.Key
		})), new LabelVisual
		{
			Text = "Market",
			TextSize = 25,
			Padding = new LiveChartsCore.Drawing.Padding(15),
			Paint = new SolidColorPaint(SKColors.DarkSlateGray)
		});
	}
}

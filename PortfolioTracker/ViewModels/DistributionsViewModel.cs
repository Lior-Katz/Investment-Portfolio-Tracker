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

		getDistributionDict(portfolioViewModel, out Dictionary<string, decimal> assetTypeDict, out Dictionary<string, decimal> marketDict, out Dictionary<string, decimal> sectorDict);


		_assetTypePieChart = getPieChartFromDict("Asset Type", assetTypeDict);


		_sectorPieChart = getPieChartFromDict("Sector", sectorDict);

		_marketPieChart = getPieChartFromDict("Market", marketDict);
		//_marketPieChart = testChart();
	}



	private static void getDistributionDict(PortfolioViewModel portfolioViewModel, out Dictionary<string, decimal> assetTypeDict, out Dictionary<string, decimal> marketDict, out Dictionary<string, decimal> sectorDict)
	{
		assetTypeDict = new Dictionary<string, decimal>();
		marketDict = new Dictionary<string, decimal>();
		sectorDict = new Dictionary<string, decimal>();

		foreach (HoldingViewModel holdingViewModel in portfolioViewModel.Holdings)
		{
			decimal percentage = portfolioViewModel.GetPercentageOfPortfolio(holdingViewModel.Id);
			updateDict(assetTypeDict, holdingViewModel.Type, percentage);
			updateDict(marketDict, holdingViewModel.Market, percentage);
			updateDict(sectorDict, holdingViewModel.Sector, percentage);
		}
	}

	private static void updateDict(Dictionary<string, decimal> dict, string key, decimal percentage)
	{
		if (dict.ContainsKey(key))
		{
			dict[key] += percentage;
		}
		else
		{
			dict.Add(key, percentage);
		}
	}

	private static PieChartViewModel getPieChartFromDict(string chartName, Dictionary<string, decimal> dict)
	{
		return new PieChartViewModel(new ObservableCollection<PieSeries<decimal>>(dict.Select(pair => new PieSeries<decimal>
		{
			Values = new[] { pair.Value },
			Name = pair.Key,
			Stroke = new SolidColorPaint(SKColors.Black) { StrokeThickness = 1 },
			HoverPushout = 8,

		})),
	   new LabelVisual
	   {
		   Text = chartName,
		   TextSize = 25,
		   Padding = new LiveChartsCore.Drawing.Padding(15),
		   Paint = new SolidColorPaint(SKColors.DarkSlateGray)
	   });
	}


	private PieChartViewModel testChart()
	{
		return new PieChartViewModel(new[]
		{
			new PieSeries<int> {
				Values = new[]{ 2 },
				Stroke = new SolidColorPaint(SKColors.Black) { StrokeThickness = 0.5F },
				HoverPushout = 8,
				Name = "abc"
			},

			new PieSeries<int> { Values = new[]{ 4 },
				Stroke = new SolidColorPaint(SKColors.Black) { StrokeThickness = 0.5F },
				HoverPushout = 8,
				Name = "def"
			},

			new PieSeries<int> { Values = new[]{ 1 },
				Stroke = new SolidColorPaint(SKColors.Black) { StrokeThickness = 0.5F },
				HoverPushout = 8,
				Name = "hij"
			},

			new PieSeries<int> { Values = new[]{ 4 },
				Stroke = new SolidColorPaint(SKColors.Black) { StrokeThickness = 0.5F },
				HoverPushout = 8,
				Name = "klm"
			},

			new PieSeries<int> { Values = new[]{ 3 },
				Stroke = new SolidColorPaint(SKColors.Black) { StrokeThickness = 0.5F },
				HoverPushout = 8,
				Name = "nop"
			},

			new PieSeries<int> { Values = new[]{ 1 },
				Stroke = new SolidColorPaint(SKColors.Black) { StrokeThickness = 0.5F },
				HoverPushout = 8,
				Name = "qrs"
			},

			new PieSeries<int> { Values = new[]{ 4 },
				Stroke = new SolidColorPaint(SKColors.Black) { StrokeThickness = 0.5F },
				HoverPushout = 8,
				Name = "tuv"
			},

			new PieSeries<int> { Values = new[]{ 3 },
				Stroke = new SolidColorPaint(SKColors.Black) { StrokeThickness = 0.5F },
				HoverPushout = 8,
				Name = "wx"
			},

			new PieSeries<int> { Values = new[]{ 3 },
				Stroke = new SolidColorPaint(SKColors.Black) { StrokeThickness = 0.5F },
				HoverPushout = 8,
				Name = "yz"
			},
		}, new LabelVisual
		{
			Text = "Market",
			TextSize = 25,
			Padding = new LiveChartsCore.Drawing.Padding(15),
			Paint = new SolidColorPaint(SKColors.DarkSlateGray)
		});
	}
}

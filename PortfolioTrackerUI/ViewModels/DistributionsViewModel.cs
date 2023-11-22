using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace PortfolioTracker.ViewModels;
public class DistributionsViewModel : ViewModelBase
{
	private readonly ObservableCollection<HoldingViewModel> _holdings;
	public ObservableCollection<HoldingViewModel> Holdings
	{
		get => _holdings;
	}

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
		this._holdings = portfolioViewModel.Holdings;

		_assetTypePieChart = new PieChartViewModel(new[] { 2, 4, 1, 4, 3 }.AsPieSeries(), new LabelVisual
		{
			Text = "Asset Type",
			TextSize = 25,
			Padding = new LiveChartsCore.Drawing.Padding(15),
			Paint = new SolidColorPaint(SKColors.DarkSlateGray)
		});

		_sectorPieChart = new PieChartViewModel(new[] { 6, 3, 2, 4, 3 }.AsPieSeries(), new LabelVisual
		{
			Text = "Sector",
			TextSize = 25,
			Padding = new LiveChartsCore.Drawing.Padding(15),
			Paint = new SolidColorPaint(SKColors.DarkSlateGray)
		});

		_marketPieChart = new PieChartViewModel(new[] { 1, 1, 1, 4, 3 }.AsPieSeries(), new LabelVisual
		{
			Text = "Market",
			TextSize = 25,
			Padding = new LiveChartsCore.Drawing.Padding(15),
			Paint = new SolidColorPaint(SKColors.DarkSlateGray)
		});
	}
}

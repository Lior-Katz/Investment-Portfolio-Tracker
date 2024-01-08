using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.VisualElements;
using System.Collections.Generic;

namespace PortfolioTracker.ViewModels
{
	public class LineGraphViewModel : ObservableObject
	{
		public ISeries[] Series { get; set; }

		public LabelVisual? Title { get; set; }

		public List<Axis> XAxes { get; set; }

		public LegendPosition LegendPosition { get; set; } = LegendPosition.Hidden;

		public LineGraphViewModel(ISeries[] series, List<Axis> xAxes)
		{
			this.Series = series;
			this.XAxes = xAxes;
		}

		public LineGraphViewModel(ISeries[] series, List<Axis> xAxes, LabelVisual title)
		{
			this.Series = series;
			this.XAxes = xAxes;
			this.Title = title;
		}

		public LineGraphViewModel(ISeries[] series, List<Axis> xAxes, LegendPosition legendPosition)
		{
			this.Series = series;
			this.XAxes = xAxes;
			this.LegendPosition = legendPosition;
		}

		public LineGraphViewModel(ISeries[] series, List<Axis> xAxes, LabelVisual title, LegendPosition legendPosition)
		{
			this.Series = series;
			this.XAxes = xAxes;
			this.Title = title;
			this.LegendPosition = legendPosition;
		}

	}
}

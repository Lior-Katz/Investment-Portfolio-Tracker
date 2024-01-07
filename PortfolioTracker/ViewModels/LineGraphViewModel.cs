using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
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

		public LineGraphViewModel(ISeries[] series, LabelVisual title, List<Axis> xAxes)
		{
			this.Series = series;
			this.Title = title;
			this.XAxes = xAxes;
		}

		public LineGraphViewModel(ISeries[] series, List<Axis> xAxes)
		{
			this.Series = series;
			this.XAxes = xAxes;
		}
	}
}

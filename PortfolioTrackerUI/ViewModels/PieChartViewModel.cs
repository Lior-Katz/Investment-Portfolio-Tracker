using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.VisualElements;
using System.Collections.Generic;

namespace PortfolioTracker.ViewModels
{
	public class PieChartViewModel : ObservableObject
	{
		public IEnumerable<ISeries> Series { get; set; }

		public LabelVisual Title { get; set; }

		public PieChartViewModel(IEnumerable<ISeries> series, LabelVisual title)
		{
			this.Series = series;
			this.Title = title;
		}


		// TODO: figure out color change
		//public SolidColorPaint LegendBackgroundPaint { get; set; } = new SolidColorPaint(new SKColor(205, 194, 209));
	}
}

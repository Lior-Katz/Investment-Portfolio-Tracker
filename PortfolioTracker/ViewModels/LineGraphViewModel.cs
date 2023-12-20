using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.VisualElements;

namespace PortfolioTracker.ViewModels
{
	public class LineGraphViewModel : ObservableObject
	{
		public ISeries[] Series { get; set; }

		public LabelVisual Title { get; set; }

		public LineGraphViewModel()
		{

		}
	}
}

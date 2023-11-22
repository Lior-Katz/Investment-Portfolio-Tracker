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



		//private int _share = 15;
		//public int Share
		//{
		//	get
		//	{
		//		return _share;
		//	}
		//	set
		//	{
		//		_share = value;
		//		OnPropertyChanged(nameof(Share));
		//	}
		//}
	}
}

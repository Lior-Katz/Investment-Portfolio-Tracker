using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.VisualElements;

namespace PortfolioTracker.ViewModels;

public class PieChartViewModel : ObservableObject
{
    public PieChartViewModel(IEnumerable<ISeries> series, LabelVisual title)
    {
        Series = series;
        Title = title;
    }

    public IEnumerable<ISeries> Series { get; set; }

    public LabelVisual Title { get; set; }


    // TODO: figure out color change
    //public SolidColorPaint LegendBackgroundPaint { get; set; } = new SolidColorPaint(new SKColor(205, 194, 209));
}

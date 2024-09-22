using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.VisualElements;

namespace PortfolioTracker.ViewModels;

public class LineGraphViewModel : ObservableObject
{
    public LineGraphViewModel(ISeries[] series, List<Axis> xAxes)
    {
        Series = series;
        XAxes = xAxes;
    }

    public LineGraphViewModel(ISeries[] series, List<Axis> xAxes, LabelVisual title)
    {
        Series = series;
        XAxes = xAxes;
        Title = title;
    }

    public LineGraphViewModel(ISeries[] series, List<Axis> xAxes, LegendPosition legendPosition)
    {
        Series = series;
        XAxes = xAxes;
        LegendPosition = legendPosition;
    }

    public LineGraphViewModel(ISeries[] series, List<Axis> xAxes, LabelVisual title, LegendPosition legendPosition)
    {
        Series = series;
        XAxes = xAxes;
        Title = title;
        LegendPosition = legendPosition;
    }

    public LineGraphViewModel(ISeries[] series, List<Axis> xAxes, ZoomAndPanMode zoomAndPanMode)
    {
        Series = series;
        XAxes = xAxes;
        ZoomAndPanMode = zoomAndPanMode;
    }

    public LineGraphViewModel(ISeries[] series, List<Axis> xAxes, LabelVisual title, ZoomAndPanMode zoomAndPanMode)
    {
        Series = series;
        XAxes = xAxes;
        Title = title;
        ZoomAndPanMode = zoomAndPanMode;
    }

    public LineGraphViewModel(ISeries[] series, List<Axis> xAxes, LegendPosition legendPosition,
                              ZoomAndPanMode zoomAndPanMode)
    {
        Series = series;
        XAxes = xAxes;
        LegendPosition = legendPosition;
        ZoomAndPanMode = zoomAndPanMode;
    }

    public LineGraphViewModel(ISeries[] series, List<Axis> xAxes, LabelVisual title, LegendPosition legendPosition,
                              ZoomAndPanMode zoomAndPanMode)
    {
        Series = series;
        XAxes = xAxes;
        Title = title;
        LegendPosition = legendPosition;
        ZoomAndPanMode = zoomAndPanMode;
    }

    public ISeries[] Series { get; set; }

    public LabelVisual? Title { get; set; }

    public List<Axis> XAxes { get; set; }

    public LegendPosition LegendPosition { get; set; } = LegendPosition.Hidden;

    public ZoomAndPanMode ZoomAndPanMode { get; set; } = ZoomAndPanMode.None;
}

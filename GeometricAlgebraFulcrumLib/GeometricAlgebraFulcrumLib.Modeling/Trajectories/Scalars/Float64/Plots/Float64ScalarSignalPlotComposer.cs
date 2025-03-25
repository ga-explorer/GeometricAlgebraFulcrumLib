using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using PngEncoder = SixLabors.ImageSharp.Formats.Png.PngEncoder;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Plots;

public sealed class Float64ScalarSignalPlotComposer
{
    public string WorkingFolder { get; }

    public int Width { get; }

    public int Height { get; }

    public double ValueRangeExpandingFactor { get; init; } = 0.1;

    public double TimeRangeFiniteExpandingFactor { get; init; } = 0.6;

    public double TimeRangePeriodicExpandingFactor { get; init; } = 2.2;

    public int SampleCount { get; init; } = 1024;


    public Float64ScalarSignalPlotComposer(string workingFolder, int width = 1280, int height = 720)
    {
        WorkingFolder = workingFolder;
        Width = width;
        Height = height;
    }


    private Image PlotToImage(Func<double, double> scalarFunction, double xMin, double xMax, double yMin, double yMax)
    {
        var model = new PlotModel
        {
            Background = OxyColors.White
        };

        var dx = (xMax - xMin) / SampleCount;

        model.Axes.Add(
            new LinearAxis
            {
                Minimum = xMin - (xMax - xMin) / 20,
                Maximum = xMax + (xMax - xMin) / 20,
                Position = AxisPosition.Bottom,

                MajorGridlineColor = OxyColors.Gray,
                MajorGridlineThickness = 1,
                MajorGridlineStyle = LineStyle.Solid,

                MinorGridlineColor = OxyColors.LightGray,
                MinorGridlineThickness = 1,
                MinorGridlineStyle = LineStyle.Dot,
            }
        );

        model.Axes.Add(
            new LinearAxis
            {
                Minimum = yMin - (yMax - yMin) / 20,
                Maximum = yMax + (yMax - yMin) / 20,
                Position = AxisPosition.Left,

                MajorGridlineColor = OxyColors.Gray,
                MajorGridlineThickness = 1,
                MajorGridlineStyle = LineStyle.Solid,

                MinorGridlineColor = OxyColors.LightGray,
                MinorGridlineThickness = 1,
                MinorGridlineStyle = LineStyle.Dot,
            }
        );

        model.Series.Add(
            new FunctionSeries(scalarFunction, xMin, xMax, dx)
            {
                Color = OxyColors.Blue,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

        var renderer = new PngExporter
        {
            //Dpi = 120,
            Width = Width,
            Height = Height
        };

        using var stream = new MemoryStream();
        renderer.Export(model, stream);

        stream.Position = 0;

        return Image.Load(stream);
    }


    public Image PlotToImage(Float64ScalarSignal scalarSignal)
    {
        var timeRangeExpandFactor =
            scalarSignal.IsPeriodic
                ? TimeRangePeriodicExpandingFactor
                : TimeRangeFiniteExpandingFactor;

        var (xMin, xMax) = scalarSignal.TimeRange.ExpandByFactor(timeRangeExpandFactor);
        var (yMin, yMax) = scalarSignal.GetValueRange().ExpandByFactor(ValueRangeExpandingFactor);

        if (yMin.IsZero() && yMax.IsZero())
        {
            yMin = -ValueRangeExpandingFactor;
            yMax = ValueRangeExpandingFactor;
        }

        if (yMin.IsNearEqual(yMax, 0))
        {
            yMin -= ValueRangeExpandingFactor * yMin;
            yMax += ValueRangeExpandingFactor * yMax;
        }

        return PlotToImage(scalarSignal.GetValue, xMin, xMax, yMin, yMax);
    }

    public Image PlotToImageWithDerivatives(Float64ScalarSignal scalarSignal)
    {
        var scalarSignalDerivative = scalarSignal.Derivative();

        var plot1 = PlotToImage(scalarSignal);
        var plot2 = PlotToImage(scalarSignalDerivative);
        var plot3 = PlotToImage(scalarSignalDerivative.Derivative());

        return plot1
            .CloneExtendFromBottom(plot2, 20, Color.White)
            .CloneExtendFromBottom(plot3, 20, Color.White);
    }

    public Image PlotToFile(Float64ScalarSignal scalarSignal, string fileName)
    {
        var plotImage = PlotToImage(scalarSignal);

        plotImage.SaveAsPng(
            WorkingFolder.GetPngFilePath(fileName),
            new PngEncoder()
            {
                ColorType = PngColorType.RgbWithAlpha
            }
        );

        return plotImage;
    }

    public Image PlotToFileWithDerivatives(Float64ScalarSignal scalarSignal, string fileName)
    {
        var plotImage = PlotToImageWithDerivatives(scalarSignal);

        plotImage.SaveAsPng(
            WorkingFolder.GetPngFilePath(fileName),
            new PngEncoder()
            {
                ColorType = PngColorType.RgbWithAlpha
            }
        );

        return plotImage;
    }
}
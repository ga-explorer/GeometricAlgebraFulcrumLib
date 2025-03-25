using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Plots;

public static class Float64ScalarSignalPlotUtils
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetMatlabPlotCode(this Float64ScalarSignal baseSignal, int sampleCount = 1024)
    {
        var (tMin, tMax) = baseSignal.TimeRange.ExpandByFactor(0.1);
        var (vMin, vMax) = baseSignal.GetValueRange().ExpandByFactor(0.1);

        return baseSignal.GetMatlabPlotCode(
            tMin, 
            tMax, 
            vMin,
            vMax,
            sampleCount
        );
    }

    public static string GetMatlabPlotCode(this Float64ScalarSignal baseSignal, double tMin, double tMax, double vMin, double vMax, int sampleCount = 1024)
    {
        var tValues =
            tMin.GetLinearRange(tMax, sampleCount).ToArray();

        var fValues =
            tValues.Select(baseSignal.GetValue).ToArray();

        var composer = new StringBuilder();

        var tArrayText =
            tValues.Select(t => t.ToString("G"))
                .Concatenate(", ", "[", "]");

        var fArrayText =
            fValues.Select(f => f.ToString("G"))
                .Concatenate(", ", "[", "]");

        var tMinText = tMin.ToString("G");
        var tMaxText = tMax.ToString("G");

        var vMinText = vMin.ToString("G");
        var vMaxText = vMax.ToString("G");

        composer.AppendLine($"t = {tArrayText};");
        composer.AppendLine($"f = {fArrayText};");
        composer.AppendLine("plot(t, f);");
        composer.AppendLine($"axis([{tMinText}, {tMaxText}, {vMinText}, {vMaxText}]);");

        return composer.ToString();
    }        
    
    
    public static Image GetPlotImage(this Func<double, double> xFunc, double t, Float64ScalarRange timeRange)
    {
        var valueRange = 
            xFunc.FindValueRange(timeRange);

        return xFunc.GetPlotImage(
            t, 
            timeRange, 
            valueRange
        );
    }

    public static Image GetPlotImage(this Func<double, double> xFunc, double t, Float64ScalarRange timeRange, Float64ScalarRange valueRange)
    {
        const int width = 1000;
        const int height = 250;
        const float resolution = 150f;

        var model = new PlotModel
        {
            Title = "Signal",
            DefaultFont = "Georgia",
            Background = OxyColors.Transparent,
            //PlotMargins = new OxyThickness(0, 0, 0, 0),
            Padding = new OxyThickness(0, 0, 0, 0)
        };

        model.Axes.Add(
            new LinearAxis
            {
                //AbsoluteMinimum = tMin,
                //AbsoluteMaximum = tMax,
                Minimum = timeRange.MinValue,
                Maximum = timeRange.MaxValue,
                Position = AxisPosition.Bottom
            }
        );

        //var signalMax = Math.Ceiling(MaxMagnitude + 0.5);
        //var signalMin = -signalMax;

        model.Axes.Add(
            new LinearAxis
            {
                //AbsoluteMinimum = SignalBounds.MinValue,
                //AbsoluteMaximum = SignalBounds.MaxValue,
                Minimum = valueRange.MinValue,
                Maximum = valueRange.MaxValue,
                Position = AxisPosition.Left
            }
        );

        var dt = timeRange.Length / 512d;
        model.Series.Add(
            new FunctionSeries(xFunc, timeRange.MinValue, timeRange.MaxValue, dt)
            {
                Color = System.Drawing.Color.DarkRed.ToOxyColor(192),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );
        
            
        model.Series.Add(
            new ScatterSeries
            {
                MarkerSize = 7,
                MarkerStroke = System.Drawing.Color.DarkRed.ToOxyColor(),
                MarkerFill = System.Drawing.Color.DarkRed.ToOxyColor(192),
                MarkerStrokeThickness = 1,
                MarkerType = MarkerType.Circle,
                Points =
                {
                    new ScatterPoint(t, xFunc(t))
                }
            }
        );

        var pngExporter = new PngExporter
        {
            Width = width, 
            Height = height, 
            Dpi = resolution
        };

        using var stream = new MemoryStream();
        pngExporter.Export(model, stream);
        stream.Position = 0;

        return Image.Load(stream);

        //var base64String = Convert.ToBase64String(stream.GetBuffer());

        //return $"'data:image/png;base64,{base64String}'";

    }
    
    public static Image GetPlotImage(this Func<double, double> xFunc, Func<double, double> yFunc, double t, Float64ScalarRange timeRange)
    {
        var valueRange = 
            xFunc.FindValueRange(timeRange).OuterBoundsUnion(
                yFunc.FindValueRange(timeRange)
            );

        return xFunc.GetPlotImage(
            yFunc, 
            t, 
            timeRange, 
            valueRange
        );
    }

    public static Image GetPlotImage(this Func<double, double> xFunc, Func<double, double> yFunc, double t, Float64ScalarRange timeRange, Float64ScalarRange valueRange)
    {
        const int width = 1000;
        const int height = 250;
        const float resolution = 150f;

        //var index1 = Math.Max(1, index - plotSampleCount) + 1;
        //var index2 = Math.Min(SampleCount, index1 + plotSampleCount) - 1;

        //var t = TimeValues[index];
        //var tMin = TimeValues[index1];
        //var tMax = TimeValues[index2];

        var model = new PlotModel
        {
            Title = "Signal",
            DefaultFont = "Georgia",
            Background = OxyColors.Transparent,
            //PlotMargins = new OxyThickness(0, 0, 0, 0),
            Padding = new OxyThickness(0, 0, 0, 0)
        };

        model.Axes.Add(
            new LinearAxis
            {
                //AbsoluteMinimum = tMin,
                //AbsoluteMaximum = tMax,
                Minimum = timeRange.MinValue,
                Maximum = timeRange.MaxValue,
                Position = AxisPosition.Bottom
            }
        );

        //var signalMax = Math.Ceiling(MaxMagnitude + 0.5);
        //var signalMin = -signalMax;

        model.Axes.Add(
            new LinearAxis
            {
                //AbsoluteMinimum = SignalBounds.MinValue,
                //AbsoluteMaximum = SignalBounds.MaxValue,
                Minimum = valueRange.MinValue,
                Maximum = valueRange.MaxValue,
                Position = AxisPosition.Left
            }
        );

        var dt = timeRange.Length / 512d;
        model.Series.Add(
            new FunctionSeries(xFunc, timeRange.MinValue, timeRange.MaxValue, dt)
            {
                Color = System.Drawing.Color.DarkRed.ToOxyColor(192),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );
            
        model.Series.Add(
            new FunctionSeries(yFunc, timeRange.MinValue, timeRange.MaxValue, dt)
            {
                Color = System.Drawing.Color.DarkGreen.ToOxyColor(192),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

            
        model.Series.Add(
            new ScatterSeries
            {
                MarkerSize = 7,
                MarkerStroke = System.Drawing.Color.DarkRed.ToOxyColor(),
                MarkerFill = System.Drawing.Color.DarkRed.ToOxyColor(192),
                MarkerStrokeThickness = 1,
                MarkerType = MarkerType.Circle,
                Points =
                {
                    new ScatterPoint(t, xFunc(t))
                }
            }
        );

        model.Series.Add(
            new ScatterSeries
            {
                MarkerSize = 7,
                MarkerStroke = System.Drawing.Color.DarkGreen.ToOxyColor(),
                MarkerFill = System.Drawing.Color.DarkGreen.ToOxyColor(192),
                MarkerStrokeThickness = 1,
                MarkerType = MarkerType.Circle,
                Points =
                {
                    new ScatterPoint(t, yFunc(t))
                }
            }
        );

        var pngExporter = new PngExporter
        {
            Width = width, 
            Height = height, 
            Dpi = resolution
        };

        using var stream = new MemoryStream();
        pngExporter.Export(model, stream);
        stream.Position = 0;

        return Image.Load(stream);

        //var base64String = Convert.ToBase64String(stream.GetBuffer());

        //return $"'data:image/png;base64,{base64String}'";

    }

    public static Image GetPlotImage(this Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc, double t, Float64ScalarRange timeRange)
    {
        var valueRange = 
            xFunc.FindValueRange(timeRange).OuterBoundsUnion(
                yFunc.FindValueRange(timeRange),
                zFunc.FindValueRange(timeRange)
            );

        return xFunc.GetPlotImage(
            yFunc, 
            zFunc, 
            t, 
            timeRange, 
            valueRange
        );
    }

    public static Image GetPlotImage(this Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc, double t, Float64ScalarRange timeRange, Float64ScalarRange valueRange)
    {
        const int width = 1000;
        const int height = 250;
        const float resolution = 150f;

        var model = new PlotModel
        {
            Title = "Signal",
            DefaultFont = "Georgia",
            Background = OxyColors.Transparent,
            //PlotMargins = new OxyThickness(0, 0, 0, 0),
            Padding = new OxyThickness(0, 0, 0, 0)
        };

        model.Axes.Add(
            new LinearAxis
            {
                //AbsoluteMinimum = tMin,
                //AbsoluteMaximum = tMax,
                Minimum = timeRange.MinValue,
                Maximum = timeRange.MaxValue,
                Position = AxisPosition.Bottom
            }
        );

        //var signalMax = Math.Ceiling(MaxMagnitude + 0.5);
        //var signalMin = -signalMax;

        model.Axes.Add(
            new LinearAxis
            {
                //AbsoluteMinimum = SignalBounds.MinValue,
                //AbsoluteMaximum = SignalBounds.MaxValue,
                Minimum = valueRange.MinValue,
                Maximum = valueRange.MaxValue,
                Position = AxisPosition.Left
            }
        );

        var dt = timeRange.Length / 512d;
        model.Series.Add(
            new FunctionSeries(xFunc, timeRange.MinValue, timeRange.MaxValue, dt)
            {
                Color = System.Drawing.Color.DarkRed.ToOxyColor(192),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );
            
        model.Series.Add(
            new FunctionSeries(yFunc, timeRange.MinValue, timeRange.MaxValue, dt)
            {
                Color = System.Drawing.Color.DarkGreen.ToOxyColor(192),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

        model.Series.Add(
            new FunctionSeries(zFunc, timeRange.MinValue, timeRange.MaxValue, dt)
            {
                Color = System.Drawing.Color.DarkBlue.ToOxyColor(192),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

            
        model.Series.Add(
            new ScatterSeries
            {
                MarkerSize = 7,
                MarkerStroke = System.Drawing.Color.DarkRed.ToOxyColor(),
                MarkerFill = System.Drawing.Color.DarkRed.ToOxyColor(192),
                MarkerStrokeThickness = 1,
                MarkerType = MarkerType.Circle,
                Points =
                {
                    new ScatterPoint(t, xFunc(t))
                }
            }
        );

        model.Series.Add(
            new ScatterSeries
            {
                MarkerSize = 7,
                MarkerStroke = System.Drawing.Color.DarkGreen.ToOxyColor(),
                MarkerFill = System.Drawing.Color.DarkGreen.ToOxyColor(192),
                MarkerStrokeThickness = 1,
                MarkerType = MarkerType.Circle,
                Points =
                {
                    new ScatterPoint(t, yFunc(t))
                }
            }
        );

        model.Series.Add(
            new ScatterSeries
            {
                MarkerSize = 7,
                MarkerStroke = System.Drawing.Color.DarkBlue.ToOxyColor(),
                MarkerFill = System.Drawing.Color.DarkBlue.ToOxyColor(192),
                MarkerStrokeThickness = 1,
                MarkerType = MarkerType.Circle,
                Points =
                {
                    new ScatterPoint(t, zFunc(t))
                }
            }
        );

        var pngExporter = new PngExporter
        {
            Width = width, 
            Height = height, 
            Dpi = resolution
        };

        using var stream = new MemoryStream();
        pngExporter.Export(model, stream);
        stream.Position = 0;

        return Image.Load(stream);

        //var base64String = Convert.ToBase64String(stream.GetBuffer());

        //return $"'data:image/png;base64,{base64String}'";

    }
    
}
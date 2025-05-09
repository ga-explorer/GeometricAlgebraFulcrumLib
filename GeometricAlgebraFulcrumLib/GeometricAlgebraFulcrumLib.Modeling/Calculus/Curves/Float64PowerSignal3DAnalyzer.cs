using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;

public sealed class Float64PowerSignal3DAnalyzer
{
    
    private static Image GetPlotImage(string title, Func<double, double> xFunc, double t, Float64ScalarRange timeRange, Float64ScalarRange valueRange)
    {
        const int width = 1000;
        const int height = 250;
        const float resolution = 150f;

        var model = new PlotModel
        {
            Title = title,
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
            new FunctionSeries(
                xFunc, 
                timeRange.MinValue, 
                t, 
                dt
            )
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
    
    private static Image GetPlotImage(string title, Func<double, double> xFunc, Func<double, double> yFunc, double t, Float64ScalarRange timeRange, Float64ScalarRange valueRange)
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
            Title = title,
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
            new FunctionSeries(
                xFunc, 
                timeRange.MinValue, 
                t, 
                dt
            )
            {
                Color = System.Drawing.Color.DarkRed.ToOxyColor(192),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );
            
        model.Series.Add(
            new FunctionSeries(
                yFunc, 
                timeRange.MinValue, 
                t, 
                dt
            )
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
    
    private static Image GetPlotImage(string title, Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc, double t, Float64ScalarRange timeRange, Float64ScalarRange valueRange)
    {
        const int width = 1000;
        const int height = 250;
        const float resolution = 150f;

        var model = new PlotModel
        {
            Title = title,
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
            new FunctionSeries(
                xFunc, 
                timeRange.MinValue, 
                t, 
                dt
            )
            {
                Color = System.Drawing.Color.DarkRed.ToOxyColor(192),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );
            
        model.Series.Add(
            new FunctionSeries(
                yFunc, 
                timeRange.MinValue, 
                t, 
                dt
            )
            {
                Color = System.Drawing.Color.DarkGreen.ToOxyColor(192),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

        model.Series.Add(
            new FunctionSeries(
                zFunc, 
                timeRange.MinValue, 
                t, 
                dt
            )
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
                    new ScatterPoint(
                        t, 
                        xFunc(t)
                    )
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
                    new ScatterPoint(
                        t, 
                        yFunc(t)
                    )
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
                    new ScatterPoint(
                        t, 
                        zFunc(t)
                    )
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


    public Float64PowerSignal3D Signal { get; }

    public double TimeScaling { get; }

    public double SignalScaling { get; }

    public int SampleCount 
        => Signal.SampleCount;

    public Float64SampledTimeSignal TimeValues 
        => Signal.TimeValues;


    internal Float64PowerSignal3DAnalyzer(Float64PowerSignal3D signal, double timeScaling = 1, double signalScaling = 1)
    {
        Signal = signal;
        TimeScaling = timeScaling;
        SignalScaling = signalScaling;
    }
    
    
    private Image GetPlotImage(string title, Func<double, double> xFunc, int index, int plotSampleCount)
    {
        var index1 = Math.Max(1, index - plotSampleCount) + 1;
        var index2 = Math.Min(SampleCount, index1 + plotSampleCount) - 1;

        var timeRange = 
            Float64ScalarRange.Create(
                TimeValues[index1],
                TimeValues[index2]
            );
        
        var timeValues = 
            TimeValues.GetItemsBetween(index1, index).ToImmutableArray();

        var valueRange =
            Float64MedianFilter.Filter(
                timeValues.Select(xFunc),
                3
            ).FindValueRange();

        //var valueRange =
        //    xFunc.FindValueRange(timeValues);

        return GetPlotImage(
            title,
            xFunc,
            TimeValues[index], 
            timeRange,
            valueRange
        );
    }
    
    private Image GetPlotImage(string title, Func<double, double> xFunc, Func<double, double> yFunc, int index, int plotSampleCount)
    {
        var index1 = Math.Max(1, index - plotSampleCount) + 1;
        var index2 = Math.Min(SampleCount, index1 + plotSampleCount) - 1;

        var timeRange = 
            Float64ScalarRange.Create(
                TimeValues[index1],
                TimeValues[index2]
            );
        
        var timeValues = 
            TimeValues.GetItemsBetween(index1, index).ToImmutableArray();

        var xValueRange =
            Float64MedianFilter.Filter(
                timeValues.Select(xFunc),
                3
            ).FindValueRange();
        
        var yValueRange =
            Float64MedianFilter.Filter(
                timeValues.Select(yFunc),
                3
            ).FindValueRange();

        var valueRange = 
            xValueRange.OuterBoundsUnion(yValueRange);

        //var valueRange =
        //    xFunc.FindValueRange(timeValues).OuterBoundsUnion(
        //        yFunc.FindValueRange(timeValues)
        //    );

        //foreach (var t in timeValues)
        //    Console.WriteLine($"yFunc({t:F4}) = {yFunc(t):F4}");

        return GetPlotImage(
            title,
            xFunc,
            yFunc,
            TimeValues[index], 
            timeRange,
            valueRange
        );
    }
    
    private Image GetPlotImage(string title, Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc, int index, int plotSampleCount)
    {
        var index1 = Math.Max(1, index - plotSampleCount) + 1;
        var index2 = Math.Min(SampleCount, index1 + plotSampleCount) - 1;

        var timeRange = 
            Float64ScalarRange.Create(
                TimeValues[index1],
                TimeValues[index2]
            );
        
        var timeValues = 
            TimeValues.GetItemsBetween(index1, index).ToImmutableArray();

        var xValueRange =
            Float64MedianFilter.Filter(
                timeValues.Select(xFunc),
                3
            ).FindValueRange();
        
        var yValueRange =
            Float64MedianFilter.Filter(
                timeValues.Select(yFunc),
                3
            ).FindValueRange();
        
        var zValueRange =
            Float64MedianFilter.Filter(
                timeValues.Select(zFunc),
                3
            ).FindValueRange();

        var valueRange = 
            xValueRange.OuterBoundsUnion(yValueRange, zValueRange);

        //var valueRange =
        //    xFunc.FindValueRange(timeValues).OuterBoundsUnion(
        //        yFunc.FindValueRange(timeValues),
        //        zFunc.FindValueRange(timeValues)
        //    );

        return GetPlotImage(
            title,
            xFunc,
            yFunc,
            zFunc, 
            TimeValues[index], 
            timeRange,
            valueRange
        );
    }

    
    private Image GetPlotImage(string title, DifferentialFunction x, int index, int plotSampleCount)
    {
        return GetPlotImage(
            title,
            x.GetValue,
            index, 
            plotSampleCount
        );
    }

    private Image GetPlotImage(string title, IPair<DifferentialFunction> xyPair, int index, int plotSampleCount)
    {
        return GetPlotImage(
            title,
            xyPair.Item1.GetValue, 
            xyPair.Item2.GetValue, 
            index, 
            plotSampleCount
        );
    }

    private Image GetPlotImage(string title, ITriplet<DifferentialFunction> xyzTriplet, int index, int plotSampleCount)
    {
        return GetPlotImage(
            title,
            xyzTriplet.Item1.GetValue, 
            xyzTriplet.Item2.GetValue, 
            xyzTriplet.Item3.GetValue, 
            index, 
            plotSampleCount
        );
    }


    public Image GetSignalPlotImage(int index, int plotSampleCount)
    {
        return GetPlotImage(
            "Signal",
            Signal.ComponentFunctions,
            index,
            plotSampleCount
        );
    }
    
    public Image GetDerivative1PlotImage(int index, int plotSampleCount)
    {
        return GetPlotImage(
            "Signal Dt1",
            Signal.ComponentsDerivative1,
            index,
            plotSampleCount
        );
    }
    
    public Image GetDerivative2PlotImage(int index, int plotSampleCount)
    {
        return GetPlotImage(
            "Signal Dt2",
            Signal.ComponentsDerivative2,
            index,
            plotSampleCount
        );
    }
    
    public Image GetDerivative3PlotImage(int index, int plotSampleCount)
    {
        return GetPlotImage(
            "Signal Dt3",
            Signal.ComponentsDerivative3,
            index,
            plotSampleCount
        );
    }
    
    public Image GetTangentNormPlotImage(int index, int plotSampleCount)
    {
        return GetPlotImage(
            "Signal Dt1 Norm",
            Signal.TangentNormFunction,
            index,
            plotSampleCount
        );
    }
    
    public Image GetArcLengthDerivative1PlotImage(int index, int plotSampleCount)
    {
        return GetPlotImage(
            "Signal Ds1",
            Signal.GetComponentsArcLengthDerivative1(),
            index,
            plotSampleCount
        );
    }
    
    public Image GetArcLengthDerivative2PlotImage(int index, int plotSampleCount)
    {
        return GetPlotImage(
            "Signal Ds2",
            Signal.GetComponentsArcLengthDerivative2(),
            index,
            plotSampleCount
        );
    }

    public Image GetArcLengthDerivative3PlotImage(int index, int plotSampleCount)
    {
        return GetPlotImage(
            "Signal Ds2",
            Signal.GetComponentsArcLengthDerivative3(),
            index,
            plotSampleCount
        );
    }

    public Image GetArcLengthVariableDerivative1PlotImage(int index, int plotSampleCount)
    {
        return GetPlotImage(
            "sDt1",
            Signal.GetArcLengthVariableDerivative1(),
            index,
            plotSampleCount
        );
    }
    
    public Image GetArcLengthVariableDerivative2PlotImage(int index, int plotSampleCount)
    {
        return GetPlotImage(
            "sDt2",
            Signal.GetArcLengthVariableDerivative2(),
            index,
            plotSampleCount
        );
    }
    
    public Image GetArcLengthVariableDerivative3PlotImage(int index, int plotSampleCount)
    {
        return GetPlotImage(
            "sDt3",
            Signal.GetArcLengthVariableDerivative3(),
            index,
            plotSampleCount
        );
    }

    public Image GetCurvaturesPlotImage(int index, int plotSampleCount)
    {
        return GetPlotImage(
            "Curvatures",
            Signal.GetCurvature1,
            Signal.GetCurvature2,
            index,
            plotSampleCount
        );
    }

    public Image GetFrequencyHzPlotImage(string title, int index, int plotSampleCount)
    {
        const int width = 1000;
        const int height = 250;
        const float resolution = 150f;

        var index1 = Math.Max(1, index - plotSampleCount) + 1;
        var index2 = Math.Min(SampleCount, index1 + plotSampleCount) - 1;

        var t = TimeValues[index];
        var tMin = TimeValues[index1];
        var tMax = TimeValues[index2];

        var model = new PlotModel
        {
            Title = title,
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
                Minimum = tMin,
                Maximum = tMax,
                Position = AxisPosition.Bottom
            }
        );

        var frequencyHzMean =
            Signal.GetDarbouxBivectorMean(index).Norm() / Math.Tau;
        
        var timeValues = 
            TimeValues.GetItemsBetween(index1, index).ToImmutableArray();

        var valueRange =
            Float64MedianFilter.Filter(
                timeValues.Select(Signal.GetFrequencyHz),
                3
            ).FindValueRange();
        
        model.Axes.Add(
            new LinearAxis
            {
                //AbsoluteMinimum = SignalCurvatureBounds.MinValue,
                //AbsoluteMaximum = SignalCurvatureBounds.MaxValue,
                Minimum = 0,
                Maximum = valueRange.MaxValue,
                Position = AxisPosition.Left
            }
        );

        var dt = (tMax - tMin) / 512d;
        model.Series.Add(
            new FunctionSeries(Signal.GetFrequencyHz, tMin, t, dt)
            {
                Color = System.Drawing.Color.DarkRed.ToOxyColor(192),
                //CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

        model.Series.Add(
            new FunctionSeries(_ => frequencyHzMean, tMin, t, dt)
            {
                Color = System.Drawing.Color.Blue.ToOxyColor(192),
                //CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 1
            }
        );

        var v = Signal.GetFrequencyHz(t).Round(7);
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
                    new ScatterPoint(t, v)
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
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.BSplineCurveBasis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using PdfExporter = OxyPlot.SkiaSharp.PdfExporter;
using PngEncoder = SixLabors.ImageSharp.Formats.Png.PngEncoder;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics;

public static class PlottingUtils
{
    public static void SavePlot(this Float64ScalarSignal scalarSignal, string filePath)
    {
        var plotImage = scalarSignal.Plot();

        plotImage.SaveAsPng(
            filePath,
            new PngEncoder()
            {
                ColorType = PngColorType.RgbWithAlpha
            }
        );
    }

    public static Image Plot(this Float64ScalarSignal scalarSignal)
    {
        var func = (double t) => scalarSignal.GetValue(t);

        var timeExpandFactor = scalarSignal.IsPeriodic ? 2.1 : 0.6;

        var (xMin, xMax) = scalarSignal.TimeRange.ExpandByFactor(timeExpandFactor);
        var (yMin, yMax) = scalarSignal.GetValueRange().ExpandByFactor(0.1);
        
        if (yMin.IsZero() && yMax.IsZero())
        {
            yMin = -0.1;
            yMax = 0.1;
        }

        if (yMin.IsNearEqual(yMax, 0))
        {
            yMin -= 0.1 * yMin;
            yMax += 0.1 * yMax;
        }

        return func.Plot(xMin, xMax, yMin, yMax);
    }

    public static Image Plot(this Float64ScalarSignal scalarSignal, double xMin, double xMax, double yMin, double yMax)
    {
        var func = (double t) => scalarSignal.GetValue(t);

        return func.Plot(xMin, xMax, yMin, yMax);
    }

    public static Image Plot(this Float64SampledTimeSignal scalarSignal, double xMin, double xMax, double yMin, double yMax)
    {
        var func = scalarSignal.LinearInterpolation;

        return func.Plot(xMin, xMax, yMin, yMax);
    }

    public static Image Plot(this Float64SampledTimeSignal scalarSignal, double xMin, double xMax)
    {
        return ((Func<double, double>)scalarSignal.LinearInterpolation).Plot(xMin, xMax);
    }

    public static Image Plot(this Func<double, double> scalarFunction, double xMin, double xMax)
    {
        var model = new PlotModel
        {
            Background = OxyColors.White
        };

        var dx = (xMax - xMin) / 1024;
        
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
            Width = 1280,
            Height = 720
        };

        using var stream = new MemoryStream();
        renderer.Export(model, stream);

        stream.Position = 0;

        return Image.Load(stream);
    }

    public static Image Plot(this Func<double, double> scalarFunction, double xMin, double xMax, double yMin, double yMax)
    {
        var model = new PlotModel
        {
            Background = OxyColors.White
        };

        var dx = (xMax - xMin) / 1024;

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
            Width = 1280,
            Height = 720
        };

        using var stream = new MemoryStream();
        renderer.Export(model, stream);

        stream.Position = 0;

        return Image.Load(stream);
    }

    public static Image Plot(this Func<double, double> scalarFunction, IReadOnlyList<double> sampledFunction, IReadOnlyList<double> sampledXValues)
    {
        var model = new PlotModel
        {

        };

        var xMin = sampledXValues[0];
        var xMax = sampledXValues[^1];
        var dx = (xMax - xMin) / 1024;
        model.Series.Add(
            new FunctionSeries(scalarFunction, xMin, xMax, dx)
            {

            }
        );

        var scatterPoints =
            Enumerable
                .Range(0, sampledXValues.Count)
                .Select(i => new ScatterPoint(sampledXValues[i], sampledFunction[i]))
                .ToList();

        var scatterSeries = new ScatterSeries
        {
            MarkerSize = 7,
            MarkerStroke = OxyColors.Black,
            MarkerFill = OxyColors.Blue,
            MarkerStrokeThickness = 1,
            MarkerType = MarkerType.Circle
        };

        scatterSeries.Points.AddRange(scatterPoints);

        model.Series.Add(scatterSeries);

        var renderer = new PngExporter
        {
            Dpi = 300,
            Width = 1280,
            Height = 720
        };

        using var stream = new MemoryStream();
        renderer.Export(model, stream);

        stream.Position = 0;

        return Image.Load(stream);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Image PlotValue(this DifferentialFunction scalarFunction, double xMin, double xMax)
    {
        return ((Func<double, double>)scalarFunction.GetValue).Plot(xMin, xMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Image PlotFirstDerivative(this DifferentialFunction scalarFunction, double xMin, double xMax)
    {
        var func = scalarFunction.GetDerivative1().GetValue;

        return func.Plot(xMin, xMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Image PlotSecondDerivative(this DifferentialFunction scalarFunction, double xMin, double xMax)
    {
        var func = scalarFunction.GetDerivative2().GetValue;

        return func.Plot(xMin, xMax);
    }

        
    public static void PlotBasisSet(this BSplineBasisPairProductSet basisSet, string filePath, float width = 1024, float height = 768)
    {
        var pm = new PlotModel
        {
            Title = $"B-Spline Basis of Degree {basisSet.Degree}",
            Background = OxyColor.FromRgb(255,255,255)
        };

        var a = basisSet.KnotVector.FirstValue;
        var b = basisSet.KnotVector.LastValue;
        var d = (b - a) * 0.1;
        a -= d;
        b += d;

        var basisCount = basisSet.BasisSet.BasisCount;
        for (var index1 = 0; index1 < basisCount; index1++)
        {
            var i1 = index1;

            for (var index2 = 0; index2 <= index1; index2++)
            {
                var i2 = index2;

                pm.Series.Add(new FunctionSeries(
                    t => basisSet.GetValue(i1, i2, t),
                    a, b, (int) width
                ));
            }
        }
            
        PdfExporter.Export(pm, filePath, width, height);
    }
        
    public static void PlotBasisSet(this BSplineBasisSet basisSet, string filePath, float width = 1024, float height = 768)
    {
        var pm = new PlotModel
        {
            Title = $"B-Spline Basis of Degree {basisSet.Degree}",
            Background = OxyColor.FromRgb(255,255,255)
        };

        var a = basisSet.KnotVector.FirstValue;
        var b = basisSet.KnotVector.LastValue;
        var d = (b - a) * 0.1;
        a -= d;
        b += d;

        for (var index = 0; index < basisSet.BasisCount; index++)
            pm.Series.Add(new FunctionSeries(
                t => basisSet.GetValue(index, t), 
                a, b, (int) width
            ));
            
        PdfExporter.Export(pm, filePath, width, height);
        //PngExporter.Export(pm, filePath, width, height, 300);
    }

    public static void PlotBasisSetPairProducts(this BSplineBasisSet basisSet, string filePath, float width = 1024, float height = 768)
    {
        var pm = new PlotModel
        {
            Title = $"B-Spline Basis of Degree {basisSet.Degree}",
            Background = OxyColor.FromRgb(255,255,255)
        };

        var a = basisSet.KnotVector.FirstValue;
        var b = basisSet.KnotVector.LastValue;
        var d = (b - a) * 0.1;
        a -= d;
        b += d;

        for (var index1 = 0; index1 < basisSet.BasisCount; index1++)
        {
            for (var index2 = 0; index2 <= index1; index2++)
            {
                pm.Series.Add(new FunctionSeries(
                    t => basisSet.GetValue(index1, t) * basisSet.GetValue(index2, t),
                    a, b, (int) width
                ));
            }
        }
            
        PdfExporter.Export(pm, filePath, width, height);
    }
}
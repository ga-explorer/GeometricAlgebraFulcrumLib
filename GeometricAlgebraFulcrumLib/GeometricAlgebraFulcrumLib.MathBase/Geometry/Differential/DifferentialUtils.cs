using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Lite.PolynomialAlgebra.BSplineCurveBasis;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using PdfExporter = OxyPlot.SkiaSharp.PdfExporter;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential;

public static class DifferentialUtils
{
        
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
                Position = AxisPosition.Bottom
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
            Width = 1200,
            Height = 600
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
                Position = AxisPosition.Bottom
            }
        );

        model.Axes.Add(
            new LinearAxis
            {
                Minimum = yMin - (yMax - yMin) / 20,
                Maximum = yMax + (yMax - yMin) / 20,
                Position = AxisPosition.Left
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
            Width = 1200,
            Height = 600
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
            Width = 1200,
            Height = 600
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
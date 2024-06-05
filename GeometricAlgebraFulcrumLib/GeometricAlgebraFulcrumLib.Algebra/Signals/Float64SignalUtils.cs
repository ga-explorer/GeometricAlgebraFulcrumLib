using GeometricAlgebraFulcrumLib.Algebra.Polynomials;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals.Composers;
using MathNet.Numerics;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Algebra.Signals;

public static class Float64SignalUtils
{
    public static Float64Signal GetPeriodicPaddedSignal1(this Float64Signal signal, int trendSampleCount, int polynomialDegree)
    {
        var scalarProcessor = ScalarProcessorOfFloat64.Instance;

        var sampleCount = signal.Count;

        var tValues = new List<double>(trendSampleCount * 2 + sampleCount);
        var uValues = new List<double>(trendSampleCount * 2 + sampleCount);
            
        for (var i = 0; i < trendSampleCount; i++)
        {
            var sampleIndex = i + sampleCount - trendSampleCount;

            tValues.Add(sampleIndex / signal.SamplingRate);
            uValues.Add(signal[sampleIndex]);
        }

        var u1 = signal[^1];
        var u2 = signal[0];
        for (var i = 0; i < sampleCount - 1; i++)
        {
            var t = (i + 1) / (double) sampleCount;
            var u = (1 - t) * u1 + t * u2;

            var sampleIndex = i + sampleCount;

            tValues.Add(sampleIndex / signal.SamplingRate);
            uValues.Add(u);
        }
            
        for (var i = 0; i < trendSampleCount; i++)
        {
            var sampleIndex = i + 2 * sampleCount;

            tValues.Add(sampleIndex / signal.SamplingRate);
            uValues.Add(signal[i]);
        }

        var polynomial = PolynomialFunction<double>.Create(
            scalarProcessor,
            Fit.Polynomial(
                tValues.ToArray(), 
                uValues.ToArray(), 
                polynomialDegree
            )
        );

        // The padded signal always has an odd number of samples
        var paddedSignalSamples = new List<double>(signal);

        for (var i = 0; i < sampleCount - 1; i++)
        {
            var tValue = (i + sampleCount) / signal.SamplingRate;
            var uValue = polynomial.GetValue(tValue);

            paddedSignalSamples.Add(uValue);
        }

        return paddedSignalSamples.CreateSignal(signal.SamplingRate);
    }
        
    public static Float64Signal GetPeriodicPaddedSignal2(this Float64Signal signal, int trendSampleCount, int polynomialDegree, int paddingSampleCount = -1)
    {
        if (paddingSampleCount == 0)
            return signal.Concat(signal.Reverse()).CreateSignal(signal.SamplingRate);

        if (paddingSampleCount < 0)
            paddingSampleCount = signal.Count;
            
        if (trendSampleCount < 1 || trendSampleCount > signal.Count)
            throw new ArgumentOutOfRangeException(nameof(trendSampleCount));

        if (polynomialDegree < 0 || polynomialDegree > trendSampleCount / 2)
            throw new ArgumentOutOfRangeException(nameof(polynomialDegree));

        var samplingSpecs = signal.SamplingSpecs;

        var n = signal.Count;
        var m = paddingSampleCount;
        var k = trendSampleCount;

        var tValues = new List<double>(trendSampleCount * 2);
        var uValues = new List<double>(trendSampleCount * 2);

        var t1Signal = samplingSpecs.GetSampledTimeSignal(n - k, k);
        var t2Signal = samplingSpecs.GetSampledTimeSignal(m + n, k);

        for (var i = 0; i < k; i++)
        {
            tValues.Add(t1Signal[i]);
            uValues.Add(signal[n - k + i]);
        }

        var tMeanCount = 10;
        var signalMean = signal.Mean();

        var tMeanList = 
            t1Signal[^1]
                .GetLinearRange(t2Signal[0], tMeanCount + 2, false)
                .Skip(1)
                .Take(tMeanCount);

        foreach (var t in tMeanList)
        {
            tValues.Add(t);
            uValues.Add(signalMean);
        }

        for (var i = 0; i < k; i++)
        {
            tValues.Add(t2Signal[i]);
            uValues.Add(signal[n - 1 - i]);
        }
            
        //var p1 = PolynomialFunction<double>.Create(
        //    scalarProcessor,
        //    Fit.Polynomial(
        //        tValues.ToArray(), 
        //        uValues.ToArray(), 
        //        polynomialDegree
        //    )
        //);

        var p1 = DfComputedFunction.Create(
            MathNet.Numerics.Interpolation.NevillePolynomialInterpolation.InterpolateSorted(
                tValues.ToArray(), 
                uValues.ToArray()
            ).Interpolate
        );

        var p1Signal = samplingSpecs.GetSampledFunctionSignal(
            p1.GetValue, 
            n, 
            m
        );

        tValues.Clear();
        uValues.Clear();

        t1Signal = samplingSpecs.GetSampledTimeSignal(m + 2 * n - k, k);
        t2Signal = samplingSpecs.GetSampledTimeSignal(2 * m + 2 * n, k);

        for (var i = 0; i < k; i++)
        {
            tValues.Add(t1Signal[i]);
            uValues.Add(signal[k - 1 - i]);
        }
            
        tMeanList = t1Signal[^1]
            .GetLinearRange(t2Signal[0], tMeanCount + 2, false)
            .Skip(1)
            .Take(tMeanCount);

        foreach (var t in tMeanList)
        {
            tValues.Add(t);
            uValues.Add(signalMean);
        }

        for (var i = 0; i < k; i++)
        {
            tValues.Add(t2Signal[i]);
            uValues.Add(signal[i]);
        }
            
        //var p2 = PolynomialFunction<double>.Create(
        //    scalarProcessor,
        //    Fit.Polynomial(
        //        tValues.ToArray(), 
        //        uValues.ToArray(), 
        //        polynomialDegree
        //    )
        //);
            
        var p2 = DfComputedFunction.Create(
            MathNet.Numerics.Interpolation.NevillePolynomialInterpolation.InterpolateSorted(
                tValues.ToArray(), 
                uValues.ToArray()
            ).Interpolate
        );

        var p2Signal = samplingSpecs.GetSampledFunctionSignal(
            p2.GetValue, 
            m + 2 * n, 
            m
        );

        var paddedSignalSamples = new List<double>(2 * n + 2 * m);

        paddedSignalSamples.AddRange(signal);
        paddedSignalSamples.AddRange(p1Signal);
        paddedSignalSamples.AddRange(signal.Reverse());
        paddedSignalSamples.AddRange(p2Signal);

        var paddedSignal = paddedSignalSamples.CreateSignal(signal.SamplingRate);

        paddedSignal.PlotSignal(
            paddedSignal.SamplingSpecs.MinTime,
            paddedSignal.SamplingSpecs.MaxTime,
            @"D:\paddedSignal"
        );

        return paddedSignal;
    }
        
    public static void PlotSignal(this Float64Signal scalarSignal, double tMin, double tMax, string plotFileName)
    {
        var samplingSpecs = scalarSignal.SamplingSpecs;

        var pm = new PlotModel
        {
            //Title = "title",
            Background = OxyColor.FromRgb(255, 255, 255)
        };

        var s1 = new FunctionSeries(
            t => scalarSignal.LinearInterpolation(t - tMin),
            tMin,
            tMax,
            1024,
            @$"Signal 2"
        )
        {
            StrokeThickness = 1.5
        };

        pm.Series.Add(s1);
            
        //OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
        PngExporter.Export(pm, $"{plotFileName}.png", samplingSpecs.SampleCount * 2, 750, 200);
    }

    public static void PlotSignal(this Float64Signal scalarSignal1, Float64Signal scalarSignal2, double tMin, double tMax, string plotFileName)
    {
        var samplingSpecs = scalarSignal1.SamplingSpecs;

        var pm = new PlotModel
        {
            //Title = "title",
            Background = OxyColor.FromRgb(255, 255, 255)
        };

        var s1 = new FunctionSeries(
            t => scalarSignal1.LinearInterpolation(t - tMin),
            tMin,
            tMax,
            samplingSpecs.SampleCount,
            @"Signal 1"
        )
        {
            LineStyle = LineStyle.Dot,
            StrokeThickness = 1,
            //MarkerType = MarkerType.Diamond,
            //MarkerStrokeThickness = 1,
            //MarkerSize = 4
        };


        var s2 = new FunctionSeries(
            t => scalarSignal2.LinearInterpolation(t - tMin),
            tMin,
            tMax,
            samplingSpecs.SampleCount * 2,
            @$"Signal 2"
        )
        {
            StrokeThickness = 1.5
        };

        pm.Series.Add(s1);
        pm.Series.Add(s2);

        //OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
        PngExporter.Export(pm, $"{plotFileName}.png", samplingSpecs.SampleCount * 2, 750, 200);
    }

    public static void PlotScalarSignal(this Float64Signal scalarSignal, string title, string filePath)
    {
        filePath = Path.Combine(filePath);

        const int sampleTrim = 0;
        var tMin = sampleTrim / scalarSignal.SamplingRate;
        var tMax = (scalarSignal.Count - 1 - sampleTrim) / scalarSignal.SamplingRate;

        var plotWidth = 
            int.Clamp(scalarSignal.Count * 2, 1920, 7680);

        var plotHeight =
            (plotWidth * 9d / 32d).RoundToInt32();

        var pm = new PlotModel
        {
            Title = title,
            Background = OxyColor.FromRgb(255, 255, 255)
        };

        var s1 = new FunctionSeries(
            scalarSignal.LinearInterpolation,
            tMin,
            tMax,
            plotWidth
        );

        pm.Series.Add(s1);

        //OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
        PngExporter.Export(pm, filePath + ".png", plotWidth, plotHeight, 200);
    }

        
    public static Image Plot(this Func<double, double> scalarFunction, Float64Signal scalarSignal, double xMin, double xMax)
    {
        var model = new PlotModel
        {
            Background = OxyColors.White
        };

        var dx = (xMax - xMin) / 1024;

        model.Axes.Add(
            new LinearAxis()
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

        model.Series.Add(
            new FunctionSeries(scalarSignal.LinearInterpolation, xMin, xMax, dx)
            {
                Color = OxyColors.Black,
                LineStyle = LineStyle.Dot,
                StrokeThickness = 1
            }
        );

        var tSignal = scalarSignal.GetSampledTimeSignal();
        var scatterPoints =
            Enumerable
                .Range(0, scalarSignal.Count)
                .Select(i => new ScatterPoint(tSignal[i], scalarSignal[i]))
                .ToList();

        var scatterSeries = new ScatterSeries
        {
            MarkerSize = 4,
            MarkerStroke = OxyColors.Green,
            MarkerFill = OxyColors.Transparent,
            MarkerStrokeThickness = 1,
            MarkerType = MarkerType.Circle
        };

        scatterSeries.Points.AddRange(scatterPoints);

        model.Series.Add(scatterSeries);

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

    public static Image PlotValue(this DifferentialFunction scalarFunction, Float64Signal scalarSignal, double xMin, double xMax)
    {
        return ((Func<double, double>)scalarFunction.GetValue).Plot(scalarSignal, xMin, xMax);
    }

    public static Image PlotFirstDerivative(this DifferentialFunction scalarFunction, Float64Signal scalarSignal, double xMin, double xMax)
    {
        var func = scalarFunction.GetDerivative1().GetValue;

        return func.Plot(scalarSignal, xMin, xMax);
    }

    public static Image PlotSecondDerivative(this DifferentialFunction scalarFunction, Float64Signal scalarSignal, double xMin, double xMax)
    {
        var func = scalarFunction.GetDerivative2().GetValue;

        return func.Plot(scalarSignal, xMin, xMax);
    }

    public static Image Plot(this Func<double, double> scalarFunction, Float64Signal scalarSignal, double xMin, double xMax, double yMin, double yMax)
    {
        var model = new PlotModel
        {
            Background = OxyColors.White
        };

        var dx = (xMax - xMin) / 1024;

        model.Axes.Add(
            new LinearAxis()
            {
                Minimum = xMin - (xMax - xMin) / 20,
                Maximum = xMax + (xMax - xMin) / 20,
                Position = AxisPosition.Bottom
            }
        );

        model.Axes.Add(
            new LinearAxis()
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

        model.Series.Add(
            new FunctionSeries(scalarSignal.LinearInterpolation, xMin, xMax, dx)
            {
                Color = OxyColors.Black,
                LineStyle = LineStyle.LongDash,
                StrokeThickness = 1
            }
        );

        var tSignal = scalarSignal.GetSampledTimeSignal();
        var scatterPoints =
            Enumerable
                .Range(0, scalarSignal.Count)
                .Select(i => new ScatterPoint(tSignal[i], scalarSignal[i]))
                .ToList();

        var scatterSeries = new ScatterSeries
        {
            MarkerSize = 4,
            MarkerStroke = OxyColors.Green,
            MarkerFill = OxyColors.Transparent,
            MarkerStrokeThickness = 1,
            MarkerType = MarkerType.Circle
        };

        scatterSeries.Points.AddRange(scatterPoints);

        model.Series.Add(scatterSeries);

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

    public static Image PlotValue(this DifferentialFunction scalarFunction, Float64Signal scalarSignal, double xMin, double xMax, double yMin, double yMax)
    {
        var func = scalarFunction.GetValue;

        return func.Plot(scalarSignal, xMin, xMax, yMin, yMax);
    }

    public static Image PlotFirstDerivative(this DifferentialFunction scalarFunction, Float64Signal scalarSignal, double xMin, double xMax, double yMin, double yMax)
    {
        var func = scalarFunction.GetDerivative1().GetValue;

        return func.Plot(scalarSignal, xMin, xMax, yMin, yMax);
    }

    public static Image PlotSecondDerivative(this DifferentialFunction scalarFunction, Float64Signal scalarSignal, double xMin, double xMax, double yMin, double yMax)
    {
        var func = scalarFunction.GetDerivative2().GetValue;

        return func.Plot(scalarSignal, xMin, xMax, yMin, yMax);
    }
}
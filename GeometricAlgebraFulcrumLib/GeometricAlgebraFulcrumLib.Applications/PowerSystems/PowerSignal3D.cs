using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Collections;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra.Composers;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SixLabors.ImageSharp;
using WebComposerLib.Colors;
using PngExporter = OxyPlot.SkiaSharp.PngExporter;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems;

public class PowerSignal3D :
    DifferentialCurve3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PowerSignal3D Create(Float64Signal timeValues, DifferentialFunction scalarFuncX, DifferentialFunction scalarFuncY, DifferentialFunction scalarFuncZ)
    {
        var scalarNormFunc = 
            (scalarFuncX.Square() + scalarFuncY.Square() + scalarFuncZ.Square()).SquareRoot().Simplify();

        return new PowerSignal3D(
            timeValues,
            scalarFuncX,
            scalarFuncY,
            scalarFuncZ,
            scalarNormFunc
        );
    }

    
    public Float64Signal TimeValues { get; }

    public Float64SignalSamplingSpecs SamplingSpecs 
        => TimeValues.SamplingSpecs;

    public int SampleCount 
        => TimeValues.Count;

    public double TimeMaxValue 
        => TimeValues.SamplingSpecs.MaxTime;

    public double TimeDelta 
        => TimeValues.SamplingSpecs.TimeResolution;

    public double Frequency 
        => 1d / TimeMaxValue;

    public double SamplingRate
        => TimeValues.SamplingSpecs.SamplingRate;
    
    public string LaTeXCode { get; set; } = string.Empty;

    public BoundingBox3D? VectorBounds { get; private set; }

    public Float64ScalarRange ScalarBounds { get; private set; }

    public Float64ScalarRange CurvatureBounds { get; private set; }

    public AdaptiveCurve3D? SampledCurve { get; private set; }

    public IReadOnlyList<AffineFrame3D>? FrameList { get; private set; }

    public IReadOnlyList<Pair<double>>? CurvatureList { get; private set; }

    public IReadOnlyList<Float64Bivector3D>? DarbouxBivectorList { get; private set; }

    public IReadOnlyList<double>? FrequencyHzList { get; private set; }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected PowerSignal3D(Float64Signal timeValues, DifferentialFunction scalarFuncX, DifferentialFunction scalarFuncY, DifferentialFunction scalarFuncZ, DifferentialFunction tangentNorm)
        : base(new Triplet<DifferentialFunction>(scalarFuncX, scalarFuncY, scalarFuncZ), tangentNorm)
    {
        TimeValues = timeValues;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected PowerSignal3D(PowerSignal3D signal)
        : this(
            signal.TimeValues, 
            signal.XFunction, 
            signal.YFunction, 
            signal.ZFunction,
            signal.TangentNormFunction
        )
    {
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<Float64Signal> GetCurvatureSignals()
    {
        var curvaturePairs = 
            TimeValues.Select(GetCurvatures).ToImmutableArray();

        return new Pair<Float64Signal>(
            curvaturePairs.Select(p => p.Item1).CreateSignal(SamplingRate),
            curvaturePairs.Select(p => p.Item2).CreateSignal(SamplingRate)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<double> GetCurvatureSignalsAverage()
    {
        return GetCurvatureSignals().MapItems(p => p.Mean());
    }

    public AdaptiveCurve3D? GetSampledCurve()
    {
        var parameterValueRange = Float64ScalarRange.Create(0, TimeMaxValue);

        var curve = ComputedParametricCurve3D.Create(
            XFunction,
            YFunction,
            ZFunction
        );

        var sampledCurve =
            new AdaptiveCurve3D(curve, parameterValueRange)
            {
                FrameSamplingMethod = ParametricCurveLocalFrameSamplingMethod.SimpleRotation,
                FrameInterpolationMethod = ParametricCurveLocalFrameInterpolationMethod.SphericalLinearInterpolation
            };

        var options = new AdaptiveCurveSamplingOptions3D(
            18.DegreesToAngle(),
            9,
            20
        );

        return sampledCurve.GenerateTree(options);
    }

    private void ComputeBounds()
    {
        VectorBounds = 
            BoundingBox3D.CreateFromPoints(
                TimeValues.Select(GetPoint)
            );

        ScalarBounds =
            Float64ScalarRange.Create(
                Math.Min(
                    Math.Min(
                        VectorBounds.MinX, 
                        VectorBounds.MinY
                    ), 
                    VectorBounds.MinZ
                ),
                Math.Max(
                    Math.Max(
                        VectorBounds.MaxX, 
                        VectorBounds.MaxY
                    ), 
                    VectorBounds.MaxZ
                )
            );
        
        var kappaMin = CurvatureList.Aggregate(
            CurvatureList[0].Min(),
            (accumulator, item) => Math.Min(accumulator, item.Min())
        );

        var kappaMax = CurvatureList.Aggregate(
            CurvatureList[0].Max(),
            (accumulator, item) => Math.Max(accumulator, item.Max())
        );

        CurvatureBounds =
            Float64ScalarRange.Create(kappaMin, kappaMax);
    }
    
    public Float64ScalarRange GetCurvatureBounds(int index1, int index2)
    {
        if (CurvatureList is null)
            throw new InvalidOperationException();

        var count = index2 - index1 + 1;

        var kappaMin = CurvatureList.GetItems(index1, count).Aggregate(
            CurvatureList[index1].Min(),
            (accumulator, item) => Math.Min(accumulator, item.Min())
        );

        var kappaMax = CurvatureList.GetItems(index1, count).Aggregate(
            CurvatureList[index1].Max(),
            (accumulator, item) => Math.Max(accumulator, item.Max())
        );

        return Float64ScalarRange.Create(kappaMin, kappaMax);
    }

    public Float64ScalarRange GetFrequencyHzBounds(int index1, int index2)
    {
        if (FrequencyHzList is null)
            throw new InvalidOperationException();

        var count = index2 - index1 + 1;

        var freqMin = FrequencyHzList.GetItems(index1, count).Aggregate(
            FrequencyHzList[index1],
            Math.Min
        );

        var freqMax = FrequencyHzList.GetItems(index1, count).Aggregate(
            FrequencyHzList[index1],
            Math.Max
        );

        return Float64ScalarRange.Create(freqMin, freqMax);
    }
    
    public Image GetSignalPlotImage(int index, int plotSampleCount)
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
                Minimum = tMin,
                Maximum = tMax,
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
                Minimum = ScalarBounds.MinValue,
                Maximum = ScalarBounds.MaxValue,
                Position = AxisPosition.Left
            }
        );

        var dt = (tMax - tMin) / 512d;
        model.Series.Add(
            new FunctionSeries(XFunction.GetValue, tMin, t, dt)
            {
                Color = System.Drawing.Color.DarkRed.ToOxyColor(192),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );
            
        model.Series.Add(
            new FunctionSeries(YFunction.GetValue, tMin, t, dt)
            {
                Color = System.Drawing.Color.DarkGreen.ToOxyColor(192),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

        model.Series.Add(
            new FunctionSeries(ZFunction.GetValue, tMin, t, dt)
            {
                Color = System.Drawing.Color.DarkBlue.ToOxyColor(192),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

            
        var v = GetPoint(t);
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
                    new ScatterPoint(t, v.Item1)
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
                    new ScatterPoint(t, v.Item2)
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
                    new ScatterPoint(t, v.Item3)
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

    public Image GetCurvaturesPlotImage(int index, int plotSampleCount)
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
            Title = "Curvatures",
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

        var curvatureBounds = 
            GetCurvatureBounds(index1, index2);

        model.Axes.Add(
            new LinearAxis
            {
                //AbsoluteMinimum = SignalCurvatureBounds.MinValue,
                //AbsoluteMaximum = SignalCurvatureBounds.MaxValue,
                Minimum = curvatureBounds.MinValue,
                Maximum = curvatureBounds.MaxValue,
                Position = AxisPosition.Left
            }
        );

        var dt = (tMax - tMin) / 512d;
        model.Series.Add(
            new FunctionSeries(x => GetCurvatures(x).Item1, tMin, t, dt)
            {
                Color = System.Drawing.Color.DarkRed.ToOxyColor(192),
                //CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );
            
        model.Series.Add(
            new FunctionSeries(x => GetCurvatures(x).Item2, tMin, t, dt)
            {
                Color = System.Drawing.Color.DarkBlue.ToOxyColor(192),
                //CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );
            
        var v = GetCurvatures(t);
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
                    new ScatterPoint(t, v.Item1)
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
                    new ScatterPoint(t, v.Item2)
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

    public Image GetFrequencyHzPlotImage(int index, int plotSampleCount)
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
            Title = "Frequency",
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

        var frequencyHzBounds =
            GetFrequencyHzBounds(index1, index2);

        var frequencyHzMean =
            GetDarbouxBivectorMean(index).Norm() / (2d * Math.PI);

        model.Axes.Add(
            new LinearAxis
            {
                //AbsoluteMinimum = SignalCurvatureBounds.MinValue,
                //AbsoluteMaximum = SignalCurvatureBounds.MaxValue,
                Minimum = frequencyHzBounds.MinValue,
                Maximum = frequencyHzBounds.MaxValue,
                Position = AxisPosition.Left
            }
        );

        var dt = (tMax - tMin) / 512d;
        model.Series.Add(
            new FunctionSeries(GetFrequencyHz, tMin, t, dt)
            {
                Color = System.Drawing.Color.DarkRed.ToOxyColor(192),
                //CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

        model.Series.Add(
            new FunctionSeries(_ => frequencyHzMean, tMin, tMax, dt)
            {
                Color = System.Drawing.Color.Blue.ToOxyColor(192),
                //CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 1
            }
        );

        var v = GetFrequencyHz(t).Round(7);
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

    public Float64Bivector3D GetDarbouxBivectorMean()
    {
        return GetDarbouxBivectorMean(0, SampleCount - 1);
    }

    public Float64Bivector3D GetDarbouxBivectorMean(int index)
    {
        return GetDarbouxBivectorMean(0, index);
    }

    public Float64Bivector3D GetDarbouxBivectorMean(int index1, int index2)
    {
        var dbMean = Float64Bivector3D.Zero;

        if (DarbouxBivectorList is null)
            return dbMean;

        for (var i = index1; i <= index2; i++) 
            dbMean += DarbouxBivectorList[i];

        return dbMean / (index2 - index1 + 1);
    }


    public PowerSignal3D InitializeComponents()
    {
        SampledCurve =
            GetSampledCurve();

        FrameList =
            TimeValues.Select(GetAffineFrame).ToImmutableArray();

        CurvatureList =
            TimeValues.Select(GetCurvatures).ToImmutableArray();
        
        DarbouxBivectorList =
            TimeValues.Select(GetDarbouxBivector).ToImmutableArray();

        FrequencyHzList =
            DarbouxBivectorList.CreateMappedList(db => 
                (db.Norm() / (2d * Math.PI)).Value
            );

        ComputeBounds();

        return this;
    }
}
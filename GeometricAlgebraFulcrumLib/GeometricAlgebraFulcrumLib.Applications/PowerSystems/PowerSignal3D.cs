using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Collections;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves.Sampled;
using GraphicsComposerLib.Rendering.Colors;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Calculus;
using NumericalGeometryLib.BasicMath.Frames.Space3D;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.Borders.Space1D.Immutable;
using NumericalGeometryLib.Borders.Space3D.Immutable;
using NumericalGeometryLib.GeometricAlgebra.Euclidean3D;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SixLabors.ImageSharp;
using PngExporter = OxyPlot.SkiaSharp.PngExporter;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems;

public class PowerSignal3D
{
    public static PowerSignal3D Create(ScalarSignalFloat64 timeValues, IScalarD3Function scalarFuncX, IScalarD3Function scalarFuncY, IScalarD3Function scalarFuncZ)
    {
        return new PowerSignal3D(
            timeValues,
            scalarFuncX,
            scalarFuncY,
            scalarFuncZ
        );
    }


    public ScalarSignalFloat64 TimeValues { get; }

    public SignalSamplingSpecs SamplingSpecs 
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

    public IScalarD3Function ScalarFuncX { get; }

    public IScalarD3Function ScalarFuncY { get; }

    public IScalarD3Function ScalarFuncZ { get; }

    public string LaTeXCode { get; set; } = string.Empty;

    public BoundingBox3D? VectorBounds { get; private set; }

    public BoundingBox1D ScalarBounds { get; private set; }

    public BoundingBox1D CurvatureBounds { get; private set; }

    public GrParametricCurveTree3D? SampledCurve { get; private set; }

    public IReadOnlyList<AffineFrame3D>? FrameList { get; private set; }

    public IReadOnlyList<Pair<double>>? CurvatureList { get; private set; }

    public IReadOnlyList<Ega3KVector2>? DarbouxBivectorList { get; private set; }

    public IReadOnlyList<double>? FrequencyHzList { get; private set; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected PowerSignal3D(ScalarSignalFloat64 timeValues, IScalarD3Function scalarFuncX, IScalarD3Function scalarFuncY, IScalarD3Function scalarFuncZ)
    {
        TimeValues = timeValues;

        ScalarFuncX = scalarFuncX;
        ScalarFuncY = scalarFuncY;
        ScalarFuncZ = scalarFuncZ;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected PowerSignal3D(PowerSignal3D signal)
    {
        TimeValues = signal.TimeValues;

        ScalarFuncX = signal.ScalarFuncX;
        ScalarFuncY = signal.ScalarFuncY;
        ScalarFuncZ = signal.ScalarFuncZ;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Triplet<Float64Tuple3D> GetPhaseVectors(double t)
    {
        var x = new Float64Tuple3D(ScalarFuncX.GetValue(t), 0, 0);
        var y = new Float64Tuple3D(0, ScalarFuncY.GetValue(t), 0);
        var z = new Float64Tuple3D(0, 0, ScalarFuncZ.GetValue(t));

        return new Triplet<Float64Tuple3D>(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple3D GetPoint(double t)
    {
        var x = ScalarFuncX.GetValue(t);
        var y = ScalarFuncY.GetValue(t);
        var z = ScalarFuncZ.GetValue(t);

        return new Float64Tuple3D(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple3D GetDerivative1(double t)
    {
        var xDt = ScalarFuncX.GetFirstDerivativeValue(t);
        var yDt = ScalarFuncY.GetFirstDerivativeValue(t);
        var zDt = ScalarFuncZ.GetFirstDerivativeValue(t);

        return new Float64Tuple3D(xDt, yDt, zDt);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple3D GetDerivative2(double t)
    {
        var xDt = ScalarFuncX.GetSecondDerivativeValue(t);
        var yDt = ScalarFuncY.GetSecondDerivativeValue(t);
        var zDt = ScalarFuncZ.GetSecondDerivativeValue(t);

        return new Float64Tuple3D(xDt, yDt, zDt);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple3D GetDerivative3(double t)
    {
        var xDt = ScalarFuncX.GetThirdDerivativeValue(t);
        var yDt = ScalarFuncY.GetThirdDerivativeValue(t);
        var zDt = ScalarFuncZ.GetThirdDerivativeValue(t);

        return new Float64Tuple3D(xDt, yDt, zDt);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetDerivative1Norm(double t)
    {
        var xDt = ScalarFuncX.GetFirstDerivativeValue(t);
        var yDt = ScalarFuncY.GetFirstDerivativeValue(t);
        var zDt = ScalarFuncZ.GetFirstDerivativeValue(t);

        return (xDt * xDt + yDt * yDt + zDt * zDt).Sqrt();
    }

    public AffineFrame3D GetAffineFrame(double t)
    {
        var origin = GetPoint(t);

        var vDt1 = GetDerivative1(t);
        var vDt2 = GetDerivative2(t);
        //var vDt3 = GetDerivative3(t);

        var sDt1 = vDt1.GetVectorNorm();
        var sDt2 = vDt1.VectorDot(vDt2) / sDt1;
        //var sDt3 = (vDt2.VectorDot(vDt2) + vDt1.VectorDot(vDt3) - sDt2.Square()) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
        //var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Square() - sDt1 * sDt3) * vDt1) / sDt1.Power(5);

        //return AffineFrame3D.Create(
        //    origin, 
        //    vDs1.ToUnitVector(), 
        //    vDs2.ToUnitVector(), 
        //    vDs3.ToUnitVector()
        //);

        var e1 = vDs1;
        var e2 = (vDs2 - vDs2.ProjectOnUnitVector(vDs1)).ToUnitVector();
        var e3 = e1.VectorUnitCross(e2);

        //var vDsMatrix = Matrix.Build.DenseOfArray(
        //    new [,]
        //    {
        //        { vDs1.X, vDs2.X, vDs3.X }, 
        //        { vDs1.Y, vDs2.Y, vDs3.Y }, 
        //        { vDs1.Z, vDs2.Z, vDs3.Z }
        //    }
        //);

        //var gramSchmidt = vDsMatrix.GramSchmidt();
        //var eMatrix = gramSchmidt.Q;
        ////var qDet = eMatrix.Determinant();

        //var e1 = new Tuple3D(eMatrix[0, 0], eMatrix[1, 0], eMatrix[2, 0]);
        //var e2 = new Tuple3D(eMatrix[0, 1], eMatrix[1, 1], eMatrix[2, 1]);
        //var e3 = e1.VectorUnitCross(e2);

        //if (eMatrix[2, 2].IsNearZero())
        //    throw new InvalidOperationException();

        ////if (t == 1.94)
        ////{
        ////    Console.WriteLine(vDsMatrix);
        ////    Console.WriteLine(gramSchmidt.Q);
        ////    Console.WriteLine(qDet);
        ////    Console.WriteLine(gramSchmidt.R);
        ////    Console.WriteLine();
        ////}

        return AffineFrame3D.Create(origin, e1, e2, e3);
    }

    public Pair<double> GetCurvatures(double t)
    {
        var vDt1 = GetDerivative1(t);
        var vDt2 = GetDerivative2(t);
        var vDt3 = GetDerivative3(t);

        var sDt1 = vDt1.GetVectorNorm();
        var sDt2 = vDt1.VectorDot(vDt2) / sDt1;
        var sDt3 = (vDt2.VectorDot(vDt2) + vDt1.VectorDot(vDt3) - sDt2.Square()) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
        var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Square() - sDt1 * sDt3) * vDt1) / sDt1.Power(5);

        var e1 = vDs1;

        var u2 = vDs2 - vDs2.ProjectOnUnitVector(e1);
        var e2 = u2.ToUnitVector();

        var u3 = vDs3 - vDs3.ProjectOnUnitVector(e2) - vDs3.ProjectOnUnitVector(e1);
        //var e3 = u3.ToUnitVector();

        var kappa1 = (sDt1 * u2.GetVectorNorm()).NaNInfinityToZero();
        var kappa2 = (sDt1 * u3.GetVectorNorm() / u2.GetVectorNorm()).NaNInfinityToZero();

        return new Pair<double>(kappa1, kappa2);
    }

    public Ega3KVector2 GetDarbouxBivector(double t)
    {
        var vDt1 = GetDerivative1(t);
        var vDt2 = GetDerivative2(t);
        var vDt3 = GetDerivative3(t);

        var sDt1 = vDt1.GetVectorNorm();
        var sDt2 = vDt1.VectorDot(vDt2) / sDt1;
        var sDt3 = (vDt2.VectorDot(vDt2) + vDt1.VectorDot(vDt3) - sDt2.Square()) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
        var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Square() - sDt1 * sDt3) * vDt1) / sDt1.Power(5);

        var e1 = vDs1.ToEga3KVector1();

        var u2 = vDs2 - vDs2.ProjectOnUnitVector(e1);
        var e2 = u2.ToUnitVector().ToEga3KVector1();

        var u3 = vDs3 - vDs3.ProjectOnUnitVector(e2) - vDs3.ProjectOnUnitVector(e1);
        var e3 = u3.ToUnitVector().ToEga3KVector1();

        var kappa1 = (sDt1 * u2.GetVectorNorm()).NaNInfinityToZero();
        var kappa2 = (sDt1 * u3.GetVectorNorm() / u2.GetVectorNorm()).NaNInfinityToZero();

        return kappa1 * e1.Op(e2) + kappa2 * e2.Op(e3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetFrequencyHz(double t)
    {
        var omega = GetDarbouxBivector(t);
        var omegaNorm = omega.Norm();
        var freqHz = omegaNorm / (2d * Math.PI);

        return freqHz.Round(7);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<ScalarSignalFloat64> GetCurvatureSignals()
    {
        var curvaturePairs = 
            TimeValues.Select(GetCurvatures).ToImmutableArray();

        return new Pair<ScalarSignalFloat64>(
            curvaturePairs.Select(p => p.Item1).CreateSignal(SamplingRate),
            curvaturePairs.Select(p => p.Item2).CreateSignal(SamplingRate)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<double> GetCurvatureSignalsAverage()
    {
        return GetCurvatureSignals().MapItems(p => p.Mean());
    }

    public GrParametricCurveTree3D? GetSampledCurve()
    {
        var parameterValueRange = BoundingBox1D.Create(0, TimeMaxValue);

        var curve = GrComputedParametricCurve3D.Create(
            ScalarFuncX,
            ScalarFuncY,
            ScalarFuncZ
        );

        var sampledCurve =
            new GrParametricCurveTree3D(curve, parameterValueRange)
            {
                FrameSamplingMethod = GrCurveFrameSamplingMethod.SimpleRotation,
                FrameInterpolationMethod = GrCurveFrameInterpolationMethod.SphericalLinearInterpolation
            };

        var options = new GrParametricCurveTreeOptions3D(
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
            BoundingBox1D.Create(
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
            BoundingBox1D.Create(kappaMin, kappaMax);
    }
    
    public BoundingBox1D GetCurvatureBounds(int index1, int index2)
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

        return BoundingBox1D.Create(kappaMin, kappaMax);
    }

    public BoundingBox1D GetFrequencyHzBounds(int index1, int index2)
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

        return BoundingBox1D.Create(freqMin, freqMax);
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
            new FunctionSeries(ScalarFuncX.GetValue, tMin, t, dt)
            {
                Color = System.Drawing.Color.DarkRed.ToOxyColor(192),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );
            
        model.Series.Add(
            new FunctionSeries(ScalarFuncY.GetValue, tMin, t, dt)
            {
                Color = System.Drawing.Color.DarkGreen.ToOxyColor(192),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

        model.Series.Add(
            new FunctionSeries(ScalarFuncZ.GetValue, tMin, t, dt)
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

        var v = GetFrequencyHz(t);
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

    public Ega3KVector2 GetDarbouxBivectorMean()
    {
        return GetDarbouxBivectorMean(0, SampleCount - 1);
    }

    public Ega3KVector2 GetDarbouxBivectorMean(int index)
    {
        return GetDarbouxBivectorMean(0, index);
    }

    public Ega3KVector2 GetDarbouxBivectorMean(int index1, int index2)
    {
        var dbMean = Ega3KVector2.Zero;

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
                db.Norm() / (2d * Math.PI)
            );

        ComputeBounds();

        return this;
    }
}
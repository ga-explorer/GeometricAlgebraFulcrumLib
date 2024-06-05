using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves.CatmullRom;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Interpolators;

public class DfCatmullRomSplineSignalInterpolator :
    DifferentialSignalInterpolatorFunction
{
    public static DfCatmullRomSplineSignalInterpolator Create(Float64Signal signal, int sampleIndex1, int sampleIndex2, DfCatmullRomSplineSignalInterpolatorOptions options)
    {
        if (options.BezierDegree is < 2 or > 63)
            throw new ArgumentOutOfRangeException(
                nameof(options.BezierDegree),
                "Smoothing Bezier degree must be between 2 and 63"
            );

        // Make sure number of samples is compatible with Bezier interpolation
        if ((signal.Count - 1) % options.BezierDegree != 0)
            throw new ArgumentException();
        
        // Smooth the given signal
        signal = signal.GetSmoothedSignal(options);
        var samplingSpecs = signal.SamplingSpecs;

        var sampleCount = sampleIndex2 - sampleIndex1 + 1;

        // TODO: Move bezier smoothing into signal.GetSmoothedSignal(options)
        // Spline curve that interpolates signal
        var bsSignalPointList =
            signal.GetBezierSmoothingPoints(
                samplingSpecs.GetSampledTimeSignal(),
                options.BezierDegree,
                true
            ).Skip(sampleIndex1).Take(sampleCount).ToArray();

        var bsSignalCurve =
            bsSignalPointList.CreateCatmullRomSpline2D(
                options.SplineType,
                false
            );

        // Interpolating function of the signal
        return new DfCatmullRomSplineSignalInterpolator(
            samplingSpecs,
            sampleIndex1,
            sampleIndex2,
            options,
            bsSignalCurve
        );
    }

    public static DfCatmullRomSplineSignalInterpolator Create(Float64Signal signal, DfCatmullRomSplineSignalInterpolatorOptions options)
    {
        if (options.BezierDegree is < 2 or > 63)
            throw new ArgumentOutOfRangeException(
                nameof(options.BezierDegree),
                "Smoothing Bezier degree must be between 2 and 63"
            );

        // Make sure number of samples is compatible with Bezier interpolation
        if ((signal.Count - 1) % options.BezierDegree != 0)
            throw new ArgumentException();
        
        // Smooth the given signal
        signal = signal.GetSmoothedSignal(options);
        var samplingSpecs = signal.SamplingSpecs;

        // TODO: Move bezier smoothing into signal.GetSmoothedSignal(options)
        // Spline curve that interpolates signal
        var bsSignalPointList =
            signal.GetBezierSmoothingPoints(
                samplingSpecs.GetSampledTimeSignal(),
                options.BezierDegree,
                true
            ).ToArray();

        var bsSignalCurve =
            bsSignalPointList.CreateCatmullRomSpline2D(
                options.SplineType,
                false
            );

        // Interpolating function of the signal
        return new DfCatmullRomSplineSignalInterpolator(
            samplingSpecs,
            0,
            samplingSpecs.SampleCount - 1,
            options,
            bsSignalCurve
        );
    }

    
    public DfCatmullRomSplineSignalInterpolatorOptions Options { get; }

    public CatmullRomSpline2D Curve { get; }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DfCatmullRomSplineSignalInterpolator(Float64SignalSamplingSpecs samplingSpecs, int sampleIndex1, int sampleIndex2, DfCatmullRomSplineSignalInterpolatorOptions options, CatmullRomSpline2D curve)
        : base(samplingSpecs, sampleIndex1, sampleIndex2)
    {
        Curve = curve;
        Options = options;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return Curve.GetPointYFromX(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivative1()
    {
        //const double h = 5.82076609134674e-11;
        //const double h2Inv = 8589934592;

        //var derivativeFunction = CreateD0Function(
        //    t => 
        //        (ValueFunc(t + h) - ValueFunc(t - h)) * h2Inv 
        //);

        //var derivativeFunction = CreateD0Function(
        //    t => 
        //        Differentiate.FirstDerivative(ValueFunc, t)
        //);

        //var signal = derivativeFunction.CreateSignal(
        //    Signal.SamplingSpecs.MinTime, 
        //    Signal.SamplingSpecs.MaxTime, 
        //    Signal.Count, 
        //    false
        //);

        //return CreateSmoothedCatmullRomSplineD0Function(
        //    signal, 
        //    bezierDegree, 
        //    downSampleCount, 
        //    curveType
        //);
        
        return Create(
            SamplingSpecs.GetSampledFunctionSignal(GetDerivative1Value), 
            Options
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetDerivative1Value(double x)
    {
        var (foundFlag, t) =
            Curve.TryGetParameterValueFromX(x);

        if (!foundFlag)
            return 0;

        var tangent =
            Curve.GetTangent(t);

        return tangent.Y / tangent.X;
    }
}
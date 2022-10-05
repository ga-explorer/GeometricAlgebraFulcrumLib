using System;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves.CatmullRom;
using MathNet.Numerics;
using NumericalGeometryLib.BasicMath.Calculus;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalProcessing.Functions;

public class SmoothedCatmullRomSplineD0Function :
    ComputedD0Function
{
    public static SmoothedCatmullRomSplineD0Function CreateSmoothedCatmullRomSplineD0Function(ScalarSignalFloat64 signal, int bezierDegree, int downSampleCount = 0, CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal)
    {
        if (bezierDegree is < 2 or > 63)
            throw new ArgumentOutOfRangeException(
                nameof(bezierDegree),
                "Smoothing Bezier degree must be between 2 and 63"
            );


        // Down-sampled signal
        if (downSampleCount < 2 || downSampleCount > signal.Count)
            downSampleCount = signal.Count;

        var useDownSampling = 
            downSampleCount != signal.Count;

        var dsSignal = 
            useDownSampling
                ? signal.ReSample(downSampleCount)
                : signal;

        
        // Bezier-compatible re-sampled signal
        var bsSignal = dsSignal.ReSampleForBezierSmoothing(bezierDegree);
        var bsTimeSignal = bsSignal.GetTimeValuesSignal();


        // Spline curve that interpolates signal
        var signalPointList =
            bsSignal.GetBezierSmoothingPoints(
                bsTimeSignal,
                bezierDegree,
                false
            );

        var signalCurve = 
            signalPointList.CreateCatmullRomSpline2D(
                curveType, 
                false
            );

        
        // Interpolating function of the signal
        return new SmoothedCatmullRomSplineD0Function(bsSignal, signalCurve);
    }


    public ScalarSignalFloat64 Signal { get; }

    public GrCatmullRomSpline2D Curve { get; }


    protected SmoothedCatmullRomSplineD0Function(SmoothedCatmullRomSplineD0Function valueFunction)
        : base(valueFunction.Curve.GetPointYFromX)
    {
        Signal = valueFunction.Signal;
        Curve = valueFunction.Curve;
    }

    protected SmoothedCatmullRomSplineD0Function(ScalarSignalFloat64 signal, GrCatmullRomSpline2D curve)
        : base(curve.GetPointYFromX)
    {
        Signal = signal;
        Curve = curve;
    }


    public SmoothedCatmullRomSplineD0Function CreateDerivativeFunction(int bezierDegree, int downSampleCount = 0, CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal)
    {
        var derivativeFunction = CreateD0Function(
            t => Differentiate.FirstDerivative(ValueFunc, t)
        );

        var signal = derivativeFunction.CreateSignal(
            Signal.SamplingSpecs.MinTime, 
            Signal.SamplingSpecs.MaxTime, 
            Signal.Count, 
            false
        );

        return CreateSmoothedCatmullRomSplineD0Function(
            signal, 
            bezierDegree, 
            downSampleCount, 
            curveType
        );
    }
}
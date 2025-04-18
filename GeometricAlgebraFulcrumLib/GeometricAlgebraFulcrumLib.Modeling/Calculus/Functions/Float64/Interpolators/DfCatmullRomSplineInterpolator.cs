﻿using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Composers;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;

public class DfCatmullRomSplineInterpolator :
    DifferentialInterpolatorFunction
{
    public static DfCatmullRomSplineInterpolator CreateFromSortedX(IReadOnlyList<double> xValues, IReadOnlyList<double> yValues, CatmullRomSplineType splineType = CatmullRomSplineType.Centripetal)
    {
        if (xValues.Count != yValues.Count || xValues.Count < 4)
            throw new ArgumentException();

        var pointCount = xValues.Count;
        var bsSignalPointList = new LinFloat64Vector2D[xValues.Count];

        for (var i = 0; i < pointCount; i++)
            bsSignalPointList[i] = LinFloat64Vector2D.Create((Float64Scalar)xValues[i], (Float64Scalar)yValues[i]);

        var curve =
            bsSignalPointList.FiniteCatmullRomSpline2D(
                splineType,
                false
            );

        // Interpolating function of the signal
        return new DfCatmullRomSplineInterpolator(curve);
    }

    public static DfCatmullRomSplineInterpolator Create(Float64SampledTimeSignal signal, DfCatmullRomSplineSignalInterpolatorOptions options)
    {
        if (options.BezierDegree is < 2 or > 63)
            throw new ArgumentOutOfRangeException(
                nameof(options.BezierDegree),
                "Smoothing Bezier degree must be between 2 and 63"
            );

        // Make sure number of samples is compatible with Bezier interpolation
        if ((signal.Count - 1) % options.BezierDegree != 0)
            throw new ArgumentException();

        // Smooth the given signal, if specified in options
        signal = signal.GetSmoothedSignal(options);

        // Spline curve that interpolates signal
        var bsSignalPointList =
            signal.GetBezierSmoothingPoints(
                signal.GetSampledTimeSignal(),
                options.BezierDegree,
                true
            ).ToArray();

        var bsSignalCurve =
            bsSignalPointList.FiniteCatmullRomSpline2D(
                options.SplineType,
                false
            );

        // Interpolating function of the signal
        return new DfCatmullRomSplineInterpolator(bsSignalCurve);
    }


    public CatmullRomSplineType SplineType
        => Curve.SplineType;

    public Float64CatmullRomSplinePath2D Curve { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DfCatmullRomSplineInterpolator(Float64CatmullRomSplinePath2D curve)
    {
        Curve = curve;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return Curve.GetPointYFromX(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivative1()
    {
        return GetInterpolatorDerivative1();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivativeN(int order)
    {
        return GetInterpolatorDerivativeN(order);
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfCatmullRomSplineInterpolator GetInterpolatorDerivative1()
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

        var xValues =
            Curve.ControlPoints.Select(p => p.X.ScalarValue).ToImmutableArray();

        var yValues =
            xValues.Select(GetDerivative1Value).ToImmutableArray();

        return CreateFromSortedX(
            xValues,
            yValues,
            Curve.SplineType
        );
    }

    public DfCatmullRomSplineInterpolator GetInterpolatorDerivativeN(int order)
    {
        if (order < 1)
            throw new ArgumentException(nameof(order));

        var df = this;

        while (order > 0)
        {
            df = df.GetInterpolatorDerivative1();

            order--;
        }

        return df;
    }
}
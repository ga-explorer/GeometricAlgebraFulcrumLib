using GraphicsComposerLib.Geometry.ParametricShapes.Curves.CatmullRom;
using MathNet.Numerics;
using NumericalGeometryLib.BasicMath.Calculus;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra.Functions;

public class SmoothedCatmullRomSplineD1Function :
    SmoothedCatmullRomSplineD0Function,
    IScalarD1Function
{
    public static IScalarD1Function CreateSmoothedCatmullRomSplineD1Function(ScalarSignalFloat64 signal, int bezierDegree, int downSampleCount = 0, CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal)
    {
        var valueFunction = CreateSmoothedCatmullRomSplineD0Function(
            signal,
            bezierDegree,
            downSampleCount,
            curveType
        );

        var firstDerivativeFunction = valueFunction.CreateDerivativeFunction(
            bezierDegree,
            downSampleCount,
            curveType
        );

        //return new SmoothedCatmullRomSplineD1Function(
        //    valueFunction,
        //    firstDerivativeFunction
        //);


        // Down-sampled signal
        if (downSampleCount < 2 || downSampleCount > signal.Count)
            downSampleCount = signal.Count;

        var useDownSampling = 
            downSampleCount != signal.Count;

        var dsSignal = 
            useDownSampling
                ? signal.ReSample(downSampleCount)
                : signal;
        
        var tSignal = 
            signal.GetTimeValuesSignal();

        var dsTimeSignal = 
            dsSignal.GetTimeValuesSignal();

        var c0Signal = dsSignal;
        
        var f0Signal = tSignal.MapSamples(
            t =>
            {
                var (index1, index2) = 
                    dsTimeSignal.GetSampleIndexFromTime(t);

                if (index1 == index2)
                    return c0Signal[index1];

                var c1 = c0Signal[index1];
                var c2 = c0Signal[index2];

                var t1 = dsTimeSignal[index1];
                var t2 = dsTimeSignal[index2];

                var v1 = c1 + Integrate.OnClosedInterval(
                    firstDerivativeFunction.GetValue,
                    t1,
                    t
                );
                
                var v2 = c2 - Integrate.OnClosedInterval(
                    firstDerivativeFunction.GetValue,
                    t,
                    t2
                );

                return (v1 + v2) / 2;
            }
        );

        return ComputedD1Function.CreateD1Function(
            f0Signal.LinearInterpolation,
            firstDerivativeFunction.ValueFunc
        );
    }


    public ScalarSignalFloat64 FirstDerivativeSignal { get; }

    public GrCatmullRomSpline2D FirstDerivativeCurve { get; }

    
    protected SmoothedCatmullRomSplineD1Function(SmoothedCatmullRomSplineD0Function valueFunction, SmoothedCatmullRomSplineD0Function firstDerivativeFunction)
        : base(valueFunction)
    {
        FirstDerivativeSignal = firstDerivativeFunction.Signal;
        FirstDerivativeCurve = firstDerivativeFunction.Curve;
    }

    protected SmoothedCatmullRomSplineD1Function(ScalarSignalFloat64 valueSignal, GrCatmullRomSpline2D valueCurve, ScalarSignalFloat64 firstDerivativeSignal, GrCatmullRomSpline2D firstDerivativeCurve)
        : base(valueSignal, valueCurve)
    {
        FirstDerivativeSignal = firstDerivativeSignal;
        FirstDerivativeCurve = firstDerivativeCurve;
    }


    public double GetFirstDerivativeValue(double t)
    {
        return FirstDerivativeCurve.GetPointYFromX(t);
    }
}
using GraphicsComposerLib.Geometry.ParametricShapes.Curves.CatmullRom;
using MathNet.Numerics;
using NumericalGeometryLib.BasicMath.Calculus;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalProcessing.Functions;

public class SmoothedCatmullRomSplineD2Function :
    SmoothedCatmullRomSplineD1Function,
    IScalarD2Function
{
    public static IScalarD2Function CreateSmoothedCatmullRomSplineD2Function(ScalarSignalFloat64 signal, int bezierDegree, int downSampleCount = 0, CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal)
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
        
        var secondDerivativeFunction = firstDerivativeFunction.CreateDerivativeFunction(
            bezierDegree,
            downSampleCount,
            curveType
        );

        //return new SmoothedCatmullRomSplineD2Function(
        //    valueFunction,
        //    firstDerivativeFunction,
        //    secondDerivativeFunction
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
        var c1Signal = dsTimeSignal.MapSamples(
            firstDerivativeFunction.ValueFunc
        );
        
        var f1Signal = tSignal.MapSamples(
            t =>
            {
                var (index1, index2) = 
                    dsTimeSignal.GetSampleIndexFromTime(t);

                if (index1 == index2)
                    return c1Signal[index1];
                
                var c1 = c1Signal[index1];
                var c2 = c1Signal[index2];

                var t1 = dsTimeSignal[index1];
                var t2 = dsTimeSignal[index2];

                var v1 = c1 + Integrate.OnClosedInterval(
                    secondDerivativeFunction.ValueFunc,
                    t1,
                    t
                );
                
                var v2 = c2 - Integrate.OnClosedInterval(
                    secondDerivativeFunction.ValueFunc,
                    t,
                    t2
                );

                return (v1 + v2) / 2;
            }
        );

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
                    f1Signal.LinearInterpolation,
                    t1,
                    t
                );
                
                var v2 = c2 - Integrate.OnClosedInterval(
                    f1Signal.LinearInterpolation,
                    t,
                    t2
                );

                return (v1 + v2) / 2;
            }
        );

        return ComputedD2Function.CreateD2Function(
            f0Signal.LinearInterpolation,
            f1Signal.LinearInterpolation,
            secondDerivativeFunction.ValueFunc
        );
    }


    public ScalarSignalFloat64 SecondDerivativeSignal { get; }

    public GrCatmullRomSpline2D SecondDerivativeCurve { get; }


    protected SmoothedCatmullRomSplineD2Function(SmoothedCatmullRomSplineD0Function valueFunction, SmoothedCatmullRomSplineD0Function firstDerivativeFunction, SmoothedCatmullRomSplineD0Function secondDerivativeFunction)
        : base(valueFunction, firstDerivativeFunction)
    {
        SecondDerivativeSignal = secondDerivativeFunction.Signal;
        SecondDerivativeCurve = secondDerivativeFunction.Curve;
    }

    protected SmoothedCatmullRomSplineD2Function(ScalarSignalFloat64 valueSignal, GrCatmullRomSpline2D valueCurve, ScalarSignalFloat64 firstDerivativeSignal, GrCatmullRomSpline2D firstDerivativeCurve, ScalarSignalFloat64 secondDerivativeSignal, GrCatmullRomSpline2D secondDerivativeCurve)
        : base(valueSignal, valueCurve, firstDerivativeSignal, firstDerivativeCurve)
    {
        SecondDerivativeSignal = secondDerivativeSignal;
        SecondDerivativeCurve = secondDerivativeCurve;
    }


    public double GetSecondDerivative(double t)
    {
        return SecondDerivativeCurve.GetPointYFromX(t);
    }
}
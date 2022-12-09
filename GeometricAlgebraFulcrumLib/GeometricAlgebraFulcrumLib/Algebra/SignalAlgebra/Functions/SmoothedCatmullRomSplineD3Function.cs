using GraphicsComposerLib.Geometry.ParametricShapes.Curves.CatmullRom;
using MathNet.Numerics;
using NumericalGeometryLib.BasicMath.Calculus;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra.Functions;

public class SmoothedCatmullRomSplineD3Function :
    SmoothedCatmullRomSplineD2Function,
    IScalarD3Function
{
    public static IScalarD3Function CreateSmoothedCatmullRomSplineD3Function(ScalarSignalFloat64 signal, int bezierDegree, int downSampleCount = 0, CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal)
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
        
        var thirdDerivativeFunction = secondDerivativeFunction.CreateDerivativeFunction(
            bezierDegree,
            downSampleCount,
            curveType
        );

        //return new SmoothedCatmullRomSplineD3Function(
        //    valueFunction,
        //    firstDerivativeFunction,
        //    secondDerivativeFunction,
        //    thirdDerivativeFunction
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
        var c2Signal = dsTimeSignal.MapSamples(
            secondDerivativeFunction.ValueFunc
        );

        var f2Signal = tSignal.MapSamples(
            t =>
            {
                var (index1, index2) = 
                    dsTimeSignal.GetSampleIndexFromTime(t);

                if (index1 == index2)
                    return c2Signal[index1];
                
                var c1 = c2Signal[index1];
                var c2 = c2Signal[index2];

                var t1 = dsTimeSignal[index1];
                var t2 = dsTimeSignal[index2];

                var v1 = c1 + Integrate.OnClosedInterval(
                    thirdDerivativeFunction.ValueFunc,
                    t1,
                    t
                );
                
                var v2 = c2 - Integrate.OnClosedInterval(
                    thirdDerivativeFunction.ValueFunc,
                    t,
                    t2
                );

                return (v1 + v2) / 2;
            }
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
                    f2Signal.LinearInterpolation,
                    t1,
                    t
                );
                
                var v2 = c2 - Integrate.OnClosedInterval(
                    f2Signal.LinearInterpolation,
                    t,
                    t2
                );

                return (v1 + v2) / 2;
            }
        );
        
        //var f0Signal = tSignal.MapSamples(
        //    t =>
        //    {
        //        var (index1, index2) = 
        //            dsTimeSignal.GetSampleIndexFromTime(t);

        //        if (index1 == index2)
        //            return c1Signal[index1];

        //        var c1 = c0Signal[index1];
        //        var c2 = c0Signal[index2];

        //        var t1 = dsTimeSignal[index1];
        //        var t2 = dsTimeSignal[index2];

        //        var v1 = c1 + Integrate.OnClosedInterval(
        //            f1Signal.LinearInterpolation,
        //            t1,
        //            t
        //        );
                
        //        var v2 = c2 - Integrate.OnClosedInterval(
        //            f1Signal.LinearInterpolation,
        //            t,
        //            t2
        //        );

        //        return (v1 + v2) / 2;
        //    }
        //);

        return ComputedD3Function.CreateD3Function(
            valueFunction.ValueFunc, //f0Signal.LinearInterpolation,
            f1Signal.LinearInterpolation,
            f2Signal.LinearInterpolation,
            thirdDerivativeFunction.ValueFunc
        );
    }


    public ScalarSignalFloat64 ThirdDerivativeSignal { get; }

    public GrCatmullRomSpline2D ThirdDerivativeCurve { get; }


    protected SmoothedCatmullRomSplineD3Function(SmoothedCatmullRomSplineD0Function valueFunction, SmoothedCatmullRomSplineD0Function firstDerivativeFunction, SmoothedCatmullRomSplineD0Function secondDerivativeFunction, SmoothedCatmullRomSplineD0Function thirdDerivativeFunction)
        : base(valueFunction, firstDerivativeFunction, secondDerivativeFunction)
    {
        ThirdDerivativeSignal = secondDerivativeFunction.Signal;
        ThirdDerivativeCurve = secondDerivativeFunction.Curve;
    }

    protected SmoothedCatmullRomSplineD3Function(ScalarSignalFloat64 valueSignal, GrCatmullRomSpline2D valueCurve, ScalarSignalFloat64 firstDerivativeSignal, GrCatmullRomSpline2D firstDerivativeCurve, ScalarSignalFloat64 secondDerivativeSignal, GrCatmullRomSpline2D secondDerivativeCurve, ScalarSignalFloat64 thirdDerivativeSignal, GrCatmullRomSpline2D thirdDerivativeCurve)
        : base(valueSignal, valueCurve, firstDerivativeSignal, firstDerivativeCurve, secondDerivativeSignal, secondDerivativeCurve)
    {
        ThirdDerivativeSignal = secondDerivativeSignal;
        ThirdDerivativeCurve = secondDerivativeCurve;
    }

    
    public double GetThirdDerivativeValue(double t)
    {
        return ThirdDerivativeCurve.GetPointYFromX(t);
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MathNet.Numerics;
using MathNet.Numerics.Interpolation;

namespace NumericalGeometryLib.BasicMath.Calculus;

public class AkimaSplineFunction :
    IScalarD3Function
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AkimaSplineFunction Create(IEnumerable<double> xValues, IEnumerable<double> yValues, bool xSorted = true)
    {
        var interpolator =
            xSorted
                ? CubicSpline.InterpolateAkimaSorted(xValues.ToArray(), yValues.ToArray())
                : CubicSpline.InterpolateAkima(xValues, yValues);

        //var interpolator = 
        //    xSorted
        //        ? CubicSpline.InterpolateNatural(xValues.ToArray(), yValues.ToArray())
        //        : CubicSpline.InterpolateNatural(xValues, yValues);

        return new AkimaSplineFunction(interpolator);
    }


    public CubicSpline Interpolator { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AkimaSplineFunction(CubicSpline interpolator)
    {
        Interpolator = interpolator;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(double t)
    {
        return Interpolator.Interpolate(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetFirstDerivative(double t)
    {
        return Interpolator.Differentiate(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSecondDerivative(double t)
    {
        return Interpolator.Differentiate2(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetThirdDerivative(double t)
    {
        return Differentiate.FirstDerivative(Interpolator.Differentiate2, t);
    }
}
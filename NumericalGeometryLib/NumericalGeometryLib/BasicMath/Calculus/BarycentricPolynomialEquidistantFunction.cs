using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MathNet.Numerics;
using MathNet.Numerics.Interpolation;

namespace NumericalGeometryLib.BasicMath.Calculus;

public class BarycentricPolynomialEquidistantFunction :
    IScalarD2Function
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BarycentricPolynomialEquidistantFunction Create(IEnumerable<double> xValues, IEnumerable<double> yValues)
    {
        var interpolator =
            Barycentric.InterpolatePolynomialEquidistant(
                xValues.ToArray(), 
                yValues.ToArray()
            );
        
        return new BarycentricPolynomialEquidistantFunction(interpolator);
    }


    public Barycentric Interpolator { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BarycentricPolynomialEquidistantFunction(Barycentric interpolator)
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
        return Differentiate.FirstDerivative(GetValue, t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSecondDerivative(double t)
    {
        return Differentiate.SecondDerivative(GetValue, t);
    }
}
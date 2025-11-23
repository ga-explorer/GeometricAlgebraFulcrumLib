using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;

public static class Float64ComplexUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearReal(this Complex c, double tolerance = Float64Utils.DefaultTolerance)
    {
        return c.Imaginary.IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearImaginary(this Complex c, double tolerance = Float64Utils.DefaultTolerance)
    {
        return c.Real.IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double RotateToReal(this Complex c)
    {
        return c.Real.Sign() * c.Magnitude;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this Complex c, double tolerance = Float64Utils.DefaultTolerance)
    {
        return (c.Real.Square() + c.Imaginary.Square()).IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOne(this Complex c, double tolerance = Float64Utils.DefaultTolerance)
    {
        return ((c.Real - 1d).Square() + c.Imaginary.Square()).IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearMinusOne(this Complex c, double tolerance = Float64Utils.DefaultTolerance)
    {
        return ((c.Real + 1d).Square() + c.Imaginary.Square()).IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearConjugateTo(this Complex c1, Complex c2, double tolerance = Float64Utils.DefaultTolerance)
    {
        return (c1.Real - c2.Real).IsNearZero(tolerance) &&
               (c1.Imaginary + c2.Imaginary).IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearConjugateTo(this Complex c1, double c2Real, double c2Imaginary, double tolerance = Float64Utils.DefaultTolerance)
    {
        return (c1.Real - c2Real).IsNearZero(tolerance) &&
               (c1.Imaginary + c2Imaginary).IsNearZero(tolerance);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex NthRootOfOne(this int n, int k)
    {
        var angle = Math.Tau * k.Mod(n) / n;

        return new Complex(
            Math.Cos(angle),
            Math.Sin(angle)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex NthRootOfOne(this int n)
    {
        var angle = Math.Tau / n;

        return new Complex(
            Math.Cos(angle),
            Math.Sin(angle)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex Sum(this IEnumerable<Complex> numbers)
    {
        return numbers.Aggregate(Complex.Zero, (a, b) => a + b);
    }
}
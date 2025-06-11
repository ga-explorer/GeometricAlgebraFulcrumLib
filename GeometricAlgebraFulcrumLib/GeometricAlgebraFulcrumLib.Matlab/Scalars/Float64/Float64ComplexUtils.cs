using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

public static class Float64ComplexUtils
{
    
    public static bool IsNearReal(this Complex c, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return c.Imaginary.IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearImaginary(this Complex c, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return c.Real.IsNearZero(zeroEpsilon);
    }

    
    public static double RotateToReal(this Complex c)
    {
        return c.Real.Sign() * c.Magnitude;
    }


    
    public static bool IsNearZero(this Complex c, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (c.Real.Square() + c.Imaginary.Square()).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearOne(this Complex c, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return ((c.Real - 1d).Square() + c.Imaginary.Square()).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearMinusOne(this Complex c, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return ((c.Real + 1d).Square() + c.Imaginary.Square()).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearConjugateTo(this Complex c1, Complex c2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (c1.Real - c2.Real).IsNearZero(zeroEpsilon) &&
               (c1.Imaginary + c2.Imaginary).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearConjugateTo(this Complex c1, double c2Real, double c2Imaginary, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (c1.Real - c2Real).IsNearZero(zeroEpsilon) &&
               (c1.Imaginary + c2Imaginary).IsNearZero(zeroEpsilon);
    }


    
    public static Complex NthRootOfOne(this int n, int k)
    {
        var angle = 2 * Math.PI * k.Mod(n) / n;

        return new Complex(
            Math.Cos(angle),
            Math.Sin(angle)
        );
    }

    
    public static Complex NthRootOfOne(this int n)
    {
        var angle = 2 * Math.PI / n;

        return new Complex(
            Math.Cos(angle),
            Math.Sin(angle)
        );
    }


    
    public static Complex Sum(this IEnumerable<Complex> numbers)
    {
        return numbers.Aggregate(Complex.Zero, (a, b) => a + b);
    }
}
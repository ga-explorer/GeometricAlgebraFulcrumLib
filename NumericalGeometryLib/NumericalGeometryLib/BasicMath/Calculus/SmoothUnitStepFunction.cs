using System;
using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.BasicMath.Calculus;

/// <summary>
/// This function is infinitely differentiable smooth transition function between 0 and 1
/// https://www.youtube.com/watch?v=vD5g8aVscUI
/// </summary>
public sealed class SmoothUnitStepFunction :
    IScalarD3Function
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(double t)
    {
        if (t <= 0) return 0;
        if (t >= 1) return 1;

        //var e1 = Math.Exp(-1d / t);
        //var e2 = Math.Exp(-1d / (1d - t));

        //return e1 / (e1 + e2);

        var s = 1 - t;
        var x = 1 / t - 1 / s;
        return 1 / (1 + Math.Exp(x));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetFirstDerivativeValue(double t)
    {
        var s = 1 - t;
        var r = t * s;

        return (1 - 2 * r) * 
               Math.Exp(1 / r) /
               (r * (Math.Exp(1 / s) + Math.Exp(1 / t))).Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSecondDerivativeValue(double t)
    {
        return Math.Exp(1/(t-t.Square())) * 
            (Math.Exp(1/(1-t)) * (-1+2 * t-4 * t.Cube() + 6 * Math.Pow(t,4) - 4 * Math.Pow(t,5)) + Math.Exp(1/t) * (1 - 2 * (-1+t) * t * (-3 + 2 * t) * (1+ (-1 + t) * t)))/((Math.Exp(1/(1-t))+Math.Exp(1/t)).Cube() * Math.Pow(-1+t,4) * Math.Pow(t,4));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetThirdDerivativeValue(double t)
    {
        //(1/((E^(1/(1 - t)) + E^(1/t))^4*(-1 + t)^6*t^6))*(E^(-(3/(-1 + t)) + 1/t)*(1 + 4*E^(1/(-1 + t) + 1/t)*(-1 + (-1 + t)*t*(2 + 3*(-1 + t)*t)*(-3 + 2*(-1 + t)^2*t^2)) + 4*t^2*(-3 + t*(7 - 6*t + t^3*(5 + 3*(-2 + t)*t))) + E^(2*(1/(-1 + t) + 1/t))*(1 + 4*(-1 + t)^2*t*(-3 + (-1 + t)*t*(-9 + t*(11 + 3*(-3 + t)*t))))
        throw new NotImplementedException();
    }
    
}
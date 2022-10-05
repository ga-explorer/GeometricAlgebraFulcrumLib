using System;

namespace NumericalGeometryLib.BasicMath.Calculus;

/// <summary>
/// https://www.youtube.com/watch?v=vD5g8aVscUI
/// </summary>
public sealed class SmoothBlendD2Function :
    IScalarD2Function
{
    public double ParameterValueMin { get; }

    public double ParameterValueMax { get; }

    public IScalarDnFunction ScalarFunction1 { get; }

    public IScalarDnFunction ScalarFunction2 { get; }


    private SmoothBlendD2Function(double parameterValueMin, double parameterValueMax, IScalarDnFunction scalarFunction1, IScalarDnFunction scalarFunction2)
    {
        ParameterValueMin = parameterValueMin;
        ParameterValueMax = parameterValueMax;
        ScalarFunction1 = scalarFunction1;
        ScalarFunction2 = scalarFunction2;
    }


    private double SmoothUnitStepFunction(double t)
    {
        if (t <= ParameterValueMin) return 0;
        if (t >= ParameterValueMax) return 1;

        t = (t - ParameterValueMin) / (ParameterValueMax - ParameterValueMin);
        
        var s = 1 - t;
        var x = 1 / t - 1 / s;
        
        return 1 / (1 + Math.Exp(x));

        //var e1 = Math.Exp(-1d / t);
        //var e2 = Math.Exp(-1d / (1d - t));

        //return e1 / (e1 + e2);
    }


    public double GetValue(double t)
    {
        var x = SmoothUnitStepFunction(t);
        var y = 1 - x;

        return ScalarFunction1.GetValue(t) * y + 
               ScalarFunction2.GetValue(t) * x;
    }

    public double GetFirstDerivative(double t)
    {
        throw new NotImplementedException();
    }

    public double GetSecondDerivative(double t)
    {
        throw new NotImplementedException();
    }
}
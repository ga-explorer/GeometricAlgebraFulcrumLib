using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using System.Diagnostics;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Composed;

/// <summary>
/// https://en.wikipedia.org/wiki/Non-analytic_smooth_function#Smooth_transition_functions
/// https://www.youtube.com/watch?v=vD5g8aVscUI
/// </summary>
public sealed class TscBlend :
    TemporalFloat64Scalar
{
    internal static TscBlend Create(TemporalFloat64Scalar baseScalar1, TemporalFloat64Scalar baseScalar2, Float64Scalar blendTimeMin, Float64Scalar blendTimeMax)
    {
        return new TscBlend(baseScalar1, baseScalar2, blendTimeMin, blendTimeMax);
    }


    public TemporalFloat64Scalar BaseScalar1 { get; }

    public TemporalFloat64Scalar BaseScalar2 { get; }

    public override Float64ScalarRange TimeRange { get; }


    private TscBlend(TemporalFloat64Scalar baseScalar1, TemporalFloat64Scalar baseScalar2, double blendTimeMin, double blendTimeMax)
    {
        BaseScalar1 = baseScalar1;
        BaseScalar2 = baseScalar2;
        TimeRange = Float64ScalarRange.Create(blendTimeMin, blendTimeMax);
    }


    public override bool IsValid()
    {
        return BaseScalar1.IsValid() &&
               BaseScalar2.IsValid() &&
               MinTime.IsFinite() &&
               MaxTime.IsFinite() &&
               MinTime < MaxTime;
    }
    

    private double RampFunction(double t)
    {
        Debug.Assert(
            t >= MinTime && t <= MaxTime
        );
        
        //if (t <= TimeMin) return 0;
        //if (t >= TimeMax) return 1;

        var y = (t - MinTime) / (MaxTime - MinTime);
        
        Debug.Assert(y is >= 0 and <= 1);

        return y;
    }

    private double SmoothUnitStepFunction(double t)
    {
        Debug.Assert(
            t >= MinTime && t <= MaxTime
        );

        //if (t <= BlendTimeMin) return 0;
        //if (t >= BlendTimeMax) return 1;

        t = (t - MinTime) / (MaxTime - MinTime);

        var s = 1 - t;
        var x = 1 / t - 1 / s;

        var y = 1 / (1 + Math.Exp(x));
        
        //var e1 = Math.Exp(-1d / t);
        //var e2 = Math.Exp(-1d / (1d - t));

        //var y = e1 / (e1 + e2);

        Debug.Assert(y is >= 0 and <= 1);

        return y;
    }

    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        //var x = RampFunction(t);
        var x = SmoothUnitStepFunction(t);
        var y = 1d - x;

        return BaseScalar1.GetValue(t) * y + BaseScalar2.GetValue(t) * x;
    }

}
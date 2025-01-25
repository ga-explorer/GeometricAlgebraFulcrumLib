using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Normalized;

public sealed class TsnSmoothRectangle :
    TemporalFloat64ScalarNormalized
{
    internal static TsnSmoothRectangle Instance { get; } = new TsnSmoothRectangle();
    

    private TsnSmoothRectangle()
    {
    }


    public override bool IsValid()
    {
        return true;
    }
    
    public override double GetValue(double t)
    {
        if (t is <= -1 or >= 1) return -1;
        if (t.IsZero()) return 1;

        if (t < 0) t = -t;
        
        var s = 1 - t;
        var x = 1 / t - 1 / s;

        return 1 - 2 / (1 + Math.Exp(x));
    }

}
namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Normalized;

public sealed class TsnSmoothStep :
    TemporalFloat64ScalarNormalized
{
    internal static TsnSmoothStep Instance { get; } = new TsnSmoothStep();
    

    private TsnSmoothStep()
    {
    }


    public override bool IsValid()
    {
        return true;
    }
    
    public override double GetValue(double t)
    {
        if (t <= -1) return -1;
        if (t >= 1) return 1;

        t = (t + 1) / 2;

        var s = 1 - t;
        var x = 1 / t - 1 / s;

        return 2 / (1 + Math.Exp(x)) - 1;
    }

}
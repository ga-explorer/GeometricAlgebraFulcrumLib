namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Normalized;

public sealed class TsnRamp :
    TemporalFloat64ScalarNormalized
{
    internal static TsnRamp Instance { get; } = new TsnRamp();



    private TsnRamp()
    {
    }


    public override bool IsValid()
    {
        return true;
    }
    
    public override double GetValue(double t)
    {
        return this.TimeClamp(t);
    }

}
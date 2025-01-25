namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Normalized;

public sealed class TsnSharpStep :
    TemporalFloat64ScalarNormalized
{
    internal static TsnSharpStep Instance { get; } = new TsnSharpStep();
    

    private TsnSharpStep()
    {
    }


    public override bool IsValid()
    {
        return true;
    }
    
    public override double GetValue(double t)
    {
        return t switch
        {
            < 0 => -1,
            > 0 => 1,
            _ => 0
        };
    }

}
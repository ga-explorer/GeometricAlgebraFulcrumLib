namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Normalized;

public sealed class TsnSharpRectangle :
    TemporalFloat64ScalarNormalized
{
    internal static TsnSharpRectangle Instance { get; } = new TsnSharpRectangle();
    

    private TsnSharpRectangle()
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
            < -0.5 or > 0.5 => -1,
            > -0.5 and < 0.5 => 1,
            _ => 0
        };
    }
}
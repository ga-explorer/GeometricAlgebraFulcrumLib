using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Normalized;

public sealed class TsnHalfSin :
    TemporalFloat64ScalarNormalized
{
    internal static TsnHalfSin Instance { get; } = new TsnHalfSin();



    private TsnHalfSin()
    {
    }


    public override bool IsValid()
    {
        return true;
    }
    
    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        return (t * Math.PI / 2).Sin();
    }

}
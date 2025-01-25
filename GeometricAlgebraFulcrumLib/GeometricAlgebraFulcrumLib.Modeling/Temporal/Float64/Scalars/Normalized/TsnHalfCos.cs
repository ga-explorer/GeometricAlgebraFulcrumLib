using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Normalized;

public sealed class TsnHalfCos :
    TemporalFloat64ScalarNormalized
{
    internal static TsnHalfCos Instance { get; } = new TsnHalfCos();



    private TsnHalfCos()
    {
    }


    public override bool IsValid()
    {
        return true;
    }
    
    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        return (t * Math.PI / 2).Cos();
    }

}
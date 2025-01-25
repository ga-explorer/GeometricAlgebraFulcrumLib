using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Normalized;

public sealed class TsnFullCos :
    TemporalFloat64ScalarNormalized
{
    internal static TsnFullCos Instance { get; } = new TsnFullCos();



    private TsnFullCos()
    {
    }


    public override bool IsValid()
    {
        return true;
    }
    
    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        return (t * Math.PI).Cos();
    }

}
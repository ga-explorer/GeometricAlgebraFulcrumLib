using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Normalized;

public sealed class TsnFullSin :
    TemporalFloat64ScalarNormalized
{
    internal static TsnFullSin Instance { get; } = new TsnFullSin();



    private TsnFullSin()
    {
    }


    public override bool IsValid()
    {
        return true;
    }
    
    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        return (t * Math.PI).Sin();
    }
}
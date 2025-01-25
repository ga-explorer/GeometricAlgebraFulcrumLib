using System.Diagnostics;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Normalized;

public sealed class TsnTriangle :
    TemporalFloat64ScalarNormalized
{
    internal static TsnTriangle Default { get; }
        = new TsnTriangle(0);

    internal static TsnTriangle Create(double vertexRelativeTime)
    {
        return new TsnTriangle(2 * vertexRelativeTime - 1);
    }


    public double VertexTime { get; }


    private TsnTriangle(double vertexTime)
    {
        VertexTime = vertexTime;

        Debug.Assert(IsValid());
    }


    public override bool IsValid()
    {
        return VertexTime is >= -1 and <= 1;
    }
    
    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        return t <= VertexTime
            ? 2 * (t + 1) / (VertexTime + 1) - 1
            : 2 * (t - 1) / (VertexTime - 1) - 1;
    }

}
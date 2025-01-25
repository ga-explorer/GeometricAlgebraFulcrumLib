using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Composed;

public sealed class TscAffineMappedValue :
    TemporalFloat64Scalar
{
    public TemporalFloat64Scalar BaseScalar { get; }

    public Float64AffineMap1D ValueMap { get; }

    public override Float64ScalarRange TimeRange 
        => BaseScalar.TimeRange;


    internal TscAffineMappedValue(TemporalFloat64Scalar baseScalar, Float64AffineMap1D valueMap)
    {
        BaseScalar = baseScalar;
        ValueMap = valueMap;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseScalar.IsValid() &&
               ValueMap.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override Float64ScalarRange FindValueRange()
    {
        var (v1, v2) = BaseScalar.ValueRange;

        return Float64ScalarRange.Create(
            ValueMap[v1],
            ValueMap[v2]
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        return ValueMap.MapPoint(
            BaseScalar.GetValue(t)
        );
    }

}
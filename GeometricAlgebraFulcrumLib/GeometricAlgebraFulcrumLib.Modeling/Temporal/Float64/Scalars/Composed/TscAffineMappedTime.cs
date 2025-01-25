using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Composed;

public sealed class TscAffineMappedTime :
    TemporalFloat64Scalar
{
    public TemporalFloat64Scalar BaseScalar { get; }

    public Float64AffineMap1D TimeMap { get; }

    public Float64AffineMap1D TimeMapInverse { get; }

    
    public override Float64ScalarRange TimeRange { get; }


    internal TscAffineMappedTime(TemporalFloat64Scalar baseScalar, Float64AffineMap1D timeMap)
    {
        BaseScalar = baseScalar;
        
        TimeMap = timeMap;
        TimeMapInverse = (Float64AffineMap1D)timeMap.GetInverseAffineMap();
        
        TimeRange = TimeMap.Scaling > 0 
            ? Float64ScalarRange.Create(
                TimeMap[BaseScalar.MinTime], 
                TimeMap[BaseScalar.MaxTime]
            )
            : Float64ScalarRange.Create(
                TimeMap[BaseScalar.MaxTime], 
                TimeMap[BaseScalar.MinTime]
            );

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseScalar.IsValid() &&
               TimeMap.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override Float64ScalarRange FindValueRange()
    {
        return BaseScalar.ValueRange;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        return BaseScalar.GetValue(
            TimeMapInverse.MapPoint(t)
        );
    }

}
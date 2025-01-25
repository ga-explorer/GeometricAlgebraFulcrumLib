using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Composed;

public sealed class TscMappedValue :
    TemporalFloat64Scalar
{
    public TemporalFloat64Scalar BaseScalar { get; }

    public Func<double, double> ValueMap { get; }
    
    public override Float64ScalarRange TimeRange 
        => BaseScalar.TimeRange;


    internal TscMappedValue(TemporalFloat64Scalar baseScalar, Func<double, double> valueMap)
    {
        BaseScalar = baseScalar;
        ValueMap = valueMap;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseScalar.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        var value = ValueMap(
            BaseScalar.GetValue(t)
        );

        Debug.Assert(value.IsFinite());

        return value;
    }

}

public class TscMappedValue<T>
    : TemporalValue<T>
{
    public TemporalFloat64Scalar BaseScalar { get; }

    public Func<double, T> ValueMapFunc { get; }
        
    public override Float64ScalarRange TimeRange 
        => BaseScalar.TimeRange;


    internal TscMappedValue(TemporalFloat64Scalar baseScalar, Func<double, T> valueMapFunc)
    {
        BaseScalar = baseScalar;
        ValueMapFunc = valueMapFunc;
    }


    public override bool IsValid()
    {
        return BaseScalar.IsValid();
    }

    public override T GetValue(double t)
    {
        t = this.TimeClamp(t);

        return ValueMapFunc(BaseScalar.GetValue(t));
    }
}
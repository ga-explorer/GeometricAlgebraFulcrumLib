using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;

public sealed class TsConstant :
    TemporalFloat64Scalar
{
    internal static TsConstant CreateZero(double timeMin, double timeMax)
    {
        return new TsConstant(0, timeMin, timeMax);
    }

    internal static TsConstant Create(double value, double timeMin, double timeMax)
    {
        return new TsConstant(value, timeMin, timeMax);
    }


    public override Float64ScalarRange TimeRange { get; }

    public double Value { get; }


    private TsConstant(double value, double timeMin, double timeMax)
    {
        Value = value;
        TimeRange = Float64ScalarRange.Create(timeMin, timeMax);

        Debug.Assert(IsValid());
    }


    public override bool IsValid()
    {
        return TimeRange.IsValid() &&
               Value.IsValid() &&
               Value.IsFinite();
    }

    protected override Float64ScalarRange FindValueRange()
    {
        return Float64ScalarRange.Create(Value, Value);
    }
    
    public override double GetValue(double t)
    {
        return Value;
    }

}
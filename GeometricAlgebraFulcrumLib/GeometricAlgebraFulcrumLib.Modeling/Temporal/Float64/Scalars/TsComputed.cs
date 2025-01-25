using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;

public sealed class TsComputed :
    TemporalFloat64Scalar
{
    internal static TsComputed Create(Func<double, double> timeMapFunc, Float64ScalarRange timeRange)
    {
        return new TsComputed(timeMapFunc, timeRange);
    }

    internal static TsComputed Create(Func<double, double> timeMapFunc, double timeMin, double timeMax)
    {
        return new TsComputed(timeMapFunc, timeMin, timeMax);
    }


    public override Float64ScalarRange TimeRange { get; }

    public Func<double, double> TimeMapFunc { get; }

    
    private TsComputed(Func<double, double> timeMapFunc, Float64ScalarRange timeRange)
    {
        TimeRange = timeRange;
        TimeMapFunc = timeMapFunc;
    }

    private TsComputed(Func<double, double> timeMapFunc, double timeMin, double timeMax)
    {
        TimeRange = Float64ScalarRange.Create(timeMin, timeMax);
        TimeMapFunc = timeMapFunc;
    }
    

    public override bool IsValid()
    {
        return TimeRange.IsValid();
    }

    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        return TimeMapFunc(t);
    }
}
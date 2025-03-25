using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Colors;

public sealed class TemporalRgba32Color
    : Float64Trajectory<Rgba32>
{
    public static TemporalRgba32Color FiniteRed(Float64ScalarRange timeRange, Float64ScalarSignal red)
    {
        var zero = Float64ScalarSignal.FiniteZero(timeRange);
        var one = Float64ScalarSignal.FiniteConstant(timeRange, 1);

        return new TemporalRgba32Color(timeRange, false, red, zero, zero, one);
    }

    public static TemporalRgba32Color FiniteRed(Float64ScalarRange timeRange, Float64ScalarSignal red, Float64ScalarSignal alpha)
    {
        var zero = Float64ScalarSignal.FiniteZero(timeRange);

        return new TemporalRgba32Color(timeRange, false, red, zero, zero, alpha);
    }

    public static TemporalRgba32Color FiniteGreen(Float64ScalarRange timeRange, Float64ScalarSignal green)
    {
        var zero = Float64ScalarSignal.FiniteZero(timeRange);
        var one = Float64ScalarSignal.FiniteConstant(timeRange, 1);

        return new TemporalRgba32Color(timeRange, false, zero, green, zero, one);
    }

    public static TemporalRgba32Color FiniteGreen(Float64ScalarRange timeRange, Float64ScalarSignal green, Float64ScalarSignal alpha)
    {
        var zero = Float64ScalarSignal.FiniteZero(timeRange);

        return new TemporalRgba32Color(timeRange, false, zero, green, zero, alpha);
    }

    public static TemporalRgba32Color FiniteBlue(Float64ScalarRange timeRange, Float64ScalarSignal blue)
    {
        var zero = Float64ScalarSignal.FiniteZero(timeRange);
        var one = Float64ScalarSignal.FiniteConstant(timeRange, 1);

        return new TemporalRgba32Color(timeRange, false, zero, zero, blue, one);
    }

    public static TemporalRgba32Color FiniteBlue(Float64ScalarRange timeRange, Float64ScalarSignal blue, Float64ScalarSignal alpha)
    {
        var zero = Float64ScalarSignal.FiniteZero(timeRange);

        return new TemporalRgba32Color(timeRange, false, zero, zero, blue, alpha);
    }

    public static TemporalRgba32Color FiniteGray(Float64ScalarRange timeRange, Float64ScalarSignal gray)
    {
        var one = Float64ScalarSignal.FiniteConstant(timeRange, 1);

        return new TemporalRgba32Color(timeRange, false, gray, gray, gray, one);
    }

    public static TemporalRgba32Color FiniteGray(Float64ScalarRange timeRange, Float64ScalarSignal gray, Float64ScalarSignal alpha)
    {
        return new TemporalRgba32Color(timeRange, false, gray, gray, gray, alpha);
    }

    public static TemporalRgba32Color Finite(Float64ScalarRange timeRange, Color color, Float64ScalarSignal alpha)
    {
        var c = color.ToPixel<Rgba32>();

        return new TemporalRgba32Color(
            timeRange, 
            false, 
            (c.R / 255d).ToTimeSignal(timeRange),
            (c.G / 255d).ToTimeSignal(timeRange),
            (c.B / 255d).ToTimeSignal(timeRange),
            alpha
        );
    }

    public static TemporalRgba32Color Finite(Float64ScalarRange timeRange, Float64ScalarSignal red, Float64ScalarSignal green, Float64ScalarSignal blue)
    {
        return new TemporalRgba32Color(timeRange, false, red, green, blue, 1.ToTimeSignal(timeRange));
    }

    public static TemporalRgba32Color Finite(Float64ScalarRange timeRange, Float64ScalarSignal red, Float64ScalarSignal green, Float64ScalarSignal blue, Float64ScalarSignal alpha)
    {
        return new TemporalRgba32Color(timeRange, false, red, green, blue, alpha);
    }


    public Float64ScalarSignal Red { get; }

    public Float64ScalarSignal Green { get; }

    public Float64ScalarSignal Blue { get; }

    public Float64ScalarSignal Alpha { get; }


    private TemporalRgba32Color(Float64ScalarRange timeRange, bool isPeriodic, Float64ScalarSignal red, Float64ScalarSignal green, Float64ScalarSignal blue, Float64ScalarSignal alpha)
        : base(timeRange, isPeriodic)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }


    public override bool IsValid()
    {
        return TimeRange.IsValid() &&
               TimeRange.IsFinite &&
               Red.IsValid() &&
               Green.IsValid() &&
               Blue.IsValid() &&
               Alpha.IsValid();
    }

    public override IFloat64Trajectory ToFinite()
    {
        throw new NotImplementedException();
    }

    public override IFloat64Trajectory ToPeriodic()
    {
        throw new NotImplementedException();
    }

    public override Rgba32 GetValue(double t)
    {
        var r = (Red.GetValue(t) * 255).Clamp(0, 255).RoundToByte();
        var g = (Green.GetValue(t) * 255).Clamp(0, 255).RoundToByte();
        var b = (Blue.GetValue(t) * 255).Clamp(0, 255).RoundToByte();
        var a = (Alpha.GetValue(t) * 255).Clamp(0, 255).RoundToByte();

        return new Rgba32(r, g, b, a);
    }
}
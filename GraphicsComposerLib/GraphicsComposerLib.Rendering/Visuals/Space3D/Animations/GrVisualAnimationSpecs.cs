using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;

public sealed class GrVisualAnimationSpecs
{
    public static GrVisualAnimationSpecs Static { get; }
        = new GrVisualAnimationSpecs();
    
    public static GrVisualAnimationSpecs Create(int frameRate, Float64Range1D timeRange)
    {
        return new GrVisualAnimationSpecs(frameRate, timeRange);
    }
    
    public static GrVisualAnimationSpecs Create(int frameRate, double maxTime)
    {
        return new GrVisualAnimationSpecs(frameRate, Float64Range1D.Create(maxTime));
    }


    public Float64Range1D TimeRange { get; }

    public Int32Range1D FrameRange { get; }

    public int FrameRate { get; }

    public bool IsStatic 
        => FrameRate < 1;

    public double MinTime 
        => TimeRange.MinValue;

    public double MaxTime 
        => TimeRange.MaxValue;
    
    public int MinFrameIndex 
        => FrameRange.MinValue;
    
    public int MaxFrameIndex 
        => FrameRange.MaxValue;

    public IEnumerable<KeyValuePair<int, double>> FrameIndexTimePairs
        => FrameRange.Select(frameIndex => 
            new KeyValuePair<int, double>(
                frameIndex,
                frameIndex / (double) FrameRate
            )
        );


    private GrVisualAnimationSpecs()
    {
        FrameRate = 0;
        TimeRange = Float64Range1D.ZeroToOne;
        FrameRange = Int32Range1D.ZeroToOne;
    }

    private GrVisualAnimationSpecs(int frameRate, Float64Range1D timeRange) 
    {
        if (!timeRange.IsValid() || timeRange.MinValue < 0)
            throw new InvalidOperationException();

        if (frameRate < 1)
            throw new InvalidOperationException();

        var frameIndex1 = (int) Math.Floor(frameRate * timeRange.MinValue);
        var frameIndex2 = (int) Math.Ceiling(frameRate * timeRange.MaxValue);

        if (frameIndex1 == frameIndex2)
            throw new InvalidOperationException();

        //TODO: Test alignment of time range with frame range
        //TimeRange = Float64Range1D.Create(
        //    frameIndex1 / (double) frameRate,
        //    frameIndex2 / (double) frameRate
        //);

        FrameRate = frameRate;
        TimeRange = timeRange;
        FrameRange = Int32Range1D.Create(frameIndex1, frameIndex2);
    }
}
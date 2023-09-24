using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;

public sealed record GrVisualAnimationSpecs :
    IGeometricElement,
    IEquatable<GrVisualAnimationSpecs>
{
    public static GrVisualAnimationSpecs Static { get; }
        = new GrVisualAnimationSpecs();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimationSpecs Create(int frameRate, Float64ScalarRange timeRange)
    {
        return new GrVisualAnimationSpecs(frameRate, timeRange);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimationSpecs Create(int frameRate, double maxTime)
    {
        return new GrVisualAnimationSpecs(frameRate, Float64ScalarRange.Create(maxTime));
    }


    public Float64ScalarRange FrameTimeRange { get; }

    public Int32Range1D FrameIndexRange { get; }

    public int FrameRate { get; }
    
    public int FrameCount 
        => FrameIndexRange.Count;

    public double FrameTime 
        => 1d / FrameRate;

    public bool IsStatic 
        => FrameRate < 1;
    
    public bool IsAnimated 
        => FrameRate >= 1;

    public double MinFrameTime 
        => FrameTimeRange.MinValue;

    public double MaxFrameTime 
        => FrameTimeRange.MaxValue;
    
    public int MinFrameIndex 
        => FrameIndexRange.MinValue;
    
    public int MaxFrameIndex 
        => FrameIndexRange.MaxValue;
    
    public IEnumerable<double> FrameTimes
        => FrameIndexRange.Select(frameIndex => frameIndex * FrameTime);
    
    public IEnumerable<int> FrameIndices
        => FrameIndexRange;

    public IEnumerable<KeyValuePair<int, double>> FrameIndexTimePairs
        => FrameIndexRange.Select(frameIndex => 
            new KeyValuePair<int, double>(
                frameIndex,
                frameIndex / (double) FrameRate
            )
        );
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GrVisualAnimationSpecs()
    {
        FrameRate = 0;
        FrameTimeRange = Float64ScalarRange.ZeroToOne;
        FrameIndexRange = Int32Range1D.ZeroToOne;
    }

    private GrVisualAnimationSpecs(int frameRate, Float64ScalarRange timeRange) 
    {
        if (!timeRange.IsValid() || timeRange.MinValue < 0)
            throw new InvalidOperationException();

        if (frameRate < 1)
            throw new InvalidOperationException();

        var frameIndex1 = (int) Math.Floor(frameRate * timeRange.MinValue);
        var frameIndex2 = (int) Math.Ceiling(frameRate * timeRange.MaxValue);

        if (frameIndex1 == frameIndex2)
            throw new InvalidOperationException();

        FrameRate = frameRate;
        FrameIndexRange = Int32Range1D.Create(frameIndex1, frameIndex2);

        //TODO: Test alignment of time range with frame range
        //FrameTimeRange = timeRange;
        FrameTimeRange = Float64ScalarRange.Create(
            frameIndex1 / (double)frameRate,
            frameIndex2 / (double)frameRate
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, T>> GetFrameIndexValuePairs<T>(Func<double, T> timeToValueMapping)
    {
        return FrameIndexTimePairs.Select(
            indexTimePair => new KeyValuePair<int, T>(
                indexTimePair.Key,
                timeToValueMapping(indexTimePair.Value)
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<double, T>> GetFrameTimeValuePairs<T>(Func<double, T> timeToValueMapping)
    {
        return FrameTimes.Select(
            time => new KeyValuePair<double, T>(
                time,
                timeToValueMapping(time)
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Tuple<int, double, T>> GetFrameIndexTimeValueTuples<T>(Func<double, T> timeToValueMapping)
    {
        return FrameIndexTimePairs.Select(
            indexTimePair => new Tuple<int, double, T>(
                indexTimePair.Key,
                indexTimePair.Value,
                timeToValueMapping(indexTimePair.Value)
            )
        );
    }

    public bool Equals(GrVisualAnimationSpecs? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return FrameIndexRange.Equals(other.FrameIndexRange) && FrameRate == other.FrameRate;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(FrameIndexRange, FrameRate);
    }
}
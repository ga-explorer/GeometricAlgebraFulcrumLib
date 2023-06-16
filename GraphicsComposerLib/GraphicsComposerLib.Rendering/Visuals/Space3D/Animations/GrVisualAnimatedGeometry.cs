using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;

public abstract class GrVisualAnimatedGeometry :
    IGeometricElement
{
    public abstract Float64Range1D TimeRange { get; }
    
    public double MinTime 
        => TimeRange.MinValue;

    public double MaxTime 
        => TimeRange.MaxValue;
    

    public abstract bool IsValid();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(Float64Range1D timeRange)
    {
        return IsValid() && TimeRange == timeRange;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32Range1D GetFrameIndexRange(int frameRate)
    {
        if (frameRate < 1)
            throw new ArgumentOutOfRangeException(nameof(frameRate));

        return Int32Range1D.Create(
            (int) Math.Ceiling(TimeRange.MinValue * frameRate),
            (int) Math.Floor(TimeRange.MaxValue * frameRate)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetKeyFrameTimes(int frameRate)
    {
        var frameTime = 1d / frameRate;

        return GetFrameIndexRange(frameRate)
            .Select(
                frameIndex => frameIndex * frameTime
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetKeyFrameIndexTimePairs(int frameRate)
    {
        var frameTime = 1d / frameRate;

        return GetFrameIndexRange(frameRate)
            .Select(frameIndex => new KeyValuePair<int, double>(
                frameIndex, 
                frameIndex * frameTime
            ));
    }
}
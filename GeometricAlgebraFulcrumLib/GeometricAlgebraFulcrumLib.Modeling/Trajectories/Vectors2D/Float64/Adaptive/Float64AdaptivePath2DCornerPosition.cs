using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Adaptive;

public sealed record Float64AdaptivePath2DCornerPosition :
    IComparable<Float64AdaptivePath2DCornerPosition>
{
    public int Level { get; }

    public int SegmentCount { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64AdaptivePath2DCornerPosition(int level, int segmentCount)
    {
        Debug.Assert(level is >= 0 and <= 31);
        Debug.Assert(segmentCount >= 0 && segmentCount <= 1 << level);

        if (segmentCount == 0)
            level = 0;
        else
            while ((segmentCount & 1) == 0)
            {
                segmentCount >>= 1;
                level--;
            }

        Level = level;
        SegmentCount = segmentCount;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetGridIndex(int maxTreeLevel)
    {
        Debug.Assert(maxTreeLevel >= Level);

        return SegmentCount << maxTreeLevel - Level;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetInterpolationValue()
    {
        return SegmentCount / (double)(1 << Level);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Float64AdaptivePath2DCornerPosition? other)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));

        if (other.Level == Level)
            return SegmentCount - other.SegmentCount;

        if (other.Level > Level)
            return (SegmentCount << other.Level - Level) - other.SegmentCount;

        return SegmentCount - (other.SegmentCount << Level - other.Level);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"(L{Level}, S{SegmentCount})";
    }
}
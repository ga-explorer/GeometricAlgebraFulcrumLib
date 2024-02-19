namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Adaptive;

//public sealed record AdaptiveCurveTreeCornerIndex
//{
//    private static Tuple<int, int> SimplifyIndex(int level, int segmentCount)
//    {
//        if (segmentCount == 0)
//            level = 0;
//        else
//            while ((segmentCount & 1) == 0)
//            {
//                segmentCount >>= 1;
//                level--;
//            }

//        return new Tuple<int, int>(level, segmentCount);
//    }


//    public int Level { get; }

//    public int SegmentCount { get; }

//    public double InterpolationValue
//        => SegmentCount / (double) (1 << Level);


//    public AdaptiveCurveTreeCornerIndex(int level, int segmentCount)
//    {
//        if (level < 0 || level > 31)
//            throw new ArgumentOutOfRangeException(nameof(level));

//        if (segmentCount > (1 << level))
//            throw new ArgumentOutOfRangeException(nameof(segmentCount));

//        (Level, SegmentCount) = SimplifyIndex(level, segmentCount);
//    }


//    public int GetSampledidIndex(int maxLevel)
//    {
//        if (maxLevel < Level)
//            throw new InvalidOperationException();

//        return SegmentCount << (maxLevel - Level);
//    }
//}
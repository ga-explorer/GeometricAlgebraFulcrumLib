using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.ParametricShapes.Volumes.Sampled;

public sealed record GrParametricVolumeTreeCorner3D
{
    public GrParametricVolumeTree3D ParentTree { get; }

    public Triplet<Float64AdaptivePath3DCornerPosition> Position { get; }
        
    public int Index { get; }

    public GrParametricVolumeLocalFrame3D Frame { get; }

    public Triplet<int> GridIndex
    {
        get
        {
            var maxTreeLevel = ParentTree.TreeLevelCount;

            return new Triplet<int>(
                Position.Item1.GetGridIndex(maxTreeLevel),
                Position.Item2.GetGridIndex(maxTreeLevel),
                Position.Item3.GetGridIndex(maxTreeLevel)
            );
        }
    }

    public Triplet<double> InterpolationValue
        => new Triplet<double>(
            Position.Item1.GetInterpolationValue(),
            Position.Item2.GetInterpolationValue(),
            Position.Item3.GetInterpolationValue()
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GrParametricVolumeTreeCorner3D(GrParametricVolumeTree3D parentTree, int index, GrParametricVolumeLocalFrame3D frame, Triplet<Float64AdaptivePath3DCornerPosition> position)
    {
        ParentTree = parentTree;
        Index = index;
        Frame = frame;
        Position = position;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        var (i1, i2, i3) = GridIndex;

        return $"({i1}, {i2}, {i3})";
    }
}

//public sealed record GrParametricVolumeTreeCornerIndex
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static Pair<int> SimplifyIndex(int level, int segmentCount)
//    {
//        if (segmentCount == 0)
//            level = 0;
//        else
//            while ((segmentCount & 1) == 0)
//            {
//                segmentCount >>= 1;
//                level--;
//            }

//        return new Pair<int>(level, segmentCount);
//    }

//    public GrParametricVolumeTree3D ParentTree { get; }

//    public int Level1 { get; }

//    public int Level2 { get; }
        
//    public int Level3 { get; }

//    public int SegmentCount1 { get; }

//    public int SegmentCount2 { get; }
        
//    public int SegmentCount3 { get; }

//    public Triplet<int> GridIndex
//    {
//        get
//        {
//            var maxLevel = ParentTree.TreeLevelCount;

//            Debug.Assert(
//                maxLevel >= Level1 && maxLevel >= Level2 && maxLevel >= Level3
//            );

//            return new Triplet<int>(
//                SegmentCount1 << (maxLevel - Level1),
//                SegmentCount2 << (maxLevel - Level2),
//                SegmentCount3 << (maxLevel - Level3)
//            );
//        }
//    }

//    public Triplet<double> InterpolationValue
//        => new Triplet<double>(
//            SegmentCount1 / (double) (1 << Level1),
//            SegmentCount2 / (double) (1 << Level2),
//            SegmentCount3 / (double) (1 << Level3)
//        );


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public GrParametricVolumeTreeCornerIndex([NotNull] GrParametricVolumeTree3D parentTree, int level, int segmentCount1, int segmentCount2, int segmentCount3)
//    {
//        if (level < 0 || level > 31)
//            throw new ArgumentOutOfRangeException(nameof(level));

//        if (segmentCount1 > (1 << level))
//            throw new ArgumentOutOfRangeException(nameof(segmentCount1));
            
//        if (segmentCount2 > (1 << level))
//            throw new ArgumentOutOfRangeException(nameof(segmentCount2));
            
//        if (segmentCount3 > (1 << level))
//            throw new ArgumentOutOfRangeException(nameof(segmentCount2));

//        ParentTree = parentTree;

//        (Level1, SegmentCount1) = SimplifyIndex(level, segmentCount1);
//        (Level2, SegmentCount2) = SimplifyIndex(level, segmentCount2);
//        (Level3, SegmentCount3) = SimplifyIndex(level, segmentCount3);
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override string ToString()
//    {
//        return new StringBuilder()
//            .Append($"({SegmentCount1} / {1 << Level1}, {SegmentCount2} / {1 << Level2}, {SegmentCount3} / {1 << Level3})")
//            .ToString();
//    }
//}
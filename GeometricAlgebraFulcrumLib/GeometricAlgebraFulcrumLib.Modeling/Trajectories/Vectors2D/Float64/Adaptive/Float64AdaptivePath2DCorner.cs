using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Adaptive;

public sealed record Float64AdaptivePath2DCorner
{
    public Float64AdaptivePath2D ParentTree { get; }

    public Float64AdaptivePath2DCornerPosition Position { get; }

    public int Index { get; }

    public Float64Path2DLocalFrame Frame { get; }

    public int GridIndex
        => Position.GetGridIndex(ParentTree.TreeLevelCount);

    public double InterpolationValue
        => Position.GetInterpolationValue();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Float64AdaptivePath2DCorner(Float64AdaptivePath2D parentTree, int index, Float64Path2DLocalFrame frame, Float64AdaptivePath2DCornerPosition position)
    {
        ParentTree = parentTree;
        Index = index;
        Frame = frame;
        Position = position;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({GridIndex})";
    }
}
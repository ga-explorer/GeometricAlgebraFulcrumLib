using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;

public sealed record Float64AdaptivePath3DCorner
{
    public Float64AdaptivePath3D ParentTree { get; }

    public Float64AdaptivePath3DCornerPosition Position { get; }

    public int Index { get; }

    public Float64Path3DLocalFrame Frame { get; }

    public int GridIndex
        => Position.GetGridIndex(ParentTree.TreeLevelCount);

    public double InterpolationValue
        => Position.GetInterpolationValue();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Float64AdaptivePath3DCorner(Float64AdaptivePath3D parentTree, int index, Float64Path3DLocalFrame frame, Float64AdaptivePath3DCornerPosition position)
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
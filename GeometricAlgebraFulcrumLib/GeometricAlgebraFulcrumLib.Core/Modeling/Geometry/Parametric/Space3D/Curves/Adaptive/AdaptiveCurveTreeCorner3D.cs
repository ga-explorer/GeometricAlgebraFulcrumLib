using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves.Adaptive;

public sealed record AdaptiveCurveTreeCorner3D
{
    public AdaptiveCurve3D ParentTree { get; }

    public AdaptiveCurveTreeCornerPosition3D Position { get; }

    public int Index { get; }

    public ParametricCurveLocalFrame3D Frame { get; }

    public int GridIndex
        => Position.GetGridIndex(ParentTree.TreeLevelCount);

    public double InterpolationValue
        => Position.GetInterpolationValue();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal AdaptiveCurveTreeCorner3D(AdaptiveCurve3D parentTree, int index, ParametricCurveLocalFrame3D frame, AdaptiveCurveTreeCornerPosition3D position)
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
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Surfaces.Sampled;

public sealed record GrParametricSurfaceTreeCorner3D
{
    public GrParametricSurfaceTree3D ParentTree { get; }

    public Pair<Float64AdaptivePath3DCornerPosition> Position { get; }

    public int Index { get; }

    public GrParametricSurfaceLocalFrame3D Frame { get; }

    public Pair<int> GridIndex
    {
        get
        {
            var maxTreeLevel = ParentTree.TreeLevelCount;

            return new Pair<int>(
                Position.Item1.GetGridIndex(maxTreeLevel),
                Position.Item2.GetGridIndex(maxTreeLevel)
            );
        }
    }

    public Pair<double> InterpolationValue
        => new Pair<double>(
            Position.Item1.GetInterpolationValue(),
            Position.Item2.GetInterpolationValue()
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GrParametricSurfaceTreeCorner3D(GrParametricSurfaceTree3D parentTree, int index, GrParametricSurfaceLocalFrame3D frame, Pair<Float64AdaptivePath3DCornerPosition> position)
    {
        ParentTree = parentTree;
        Index = index;
        Frame = frame;
        Position = position;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        var (i1, i2) = GridIndex;

        return $"({i1}, {i2})";
    }
}
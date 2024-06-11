using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsMesh;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Permutations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space2D;

public sealed class PointsMeshSubsetPointsPath2D
    : PSeq2DSubset1D<ILinFloat64Vector2D>, IPointsPath2D
{
    public IPointsMesh2D BaseMesh { get; }


    internal PointsMeshSubsetPointsPath2D(IPointsMesh2D baseMesh, IIndexMap1DTo2D indexMapping)
        : base(baseMesh, indexMapping)
    {
        BaseMesh = baseMesh;
    }

        
    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }

    public IPointsPath2D MapPoints(Func<ILinFloat64Vector2D, ILinFloat64Vector2D> pointMapping)
    {
        return new ArrayPointsPath2D(
            this.Select(pointMapping).ToArray()
        );
    }
}
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsMesh;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space2D;

public sealed class PointsMeshSlicePointsPath2D
    : PSeqSlice1D<ILinFloat64Vector2D>, IPointsPath2D
{
    public IPointsMesh2D BaseMesh { get; }


    internal PointsMeshSlicePointsPath2D(IPointsMesh2D baseMesh, int sliceDimension, int sliceIndex)
        : base(baseMesh, sliceDimension, sliceIndex)
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
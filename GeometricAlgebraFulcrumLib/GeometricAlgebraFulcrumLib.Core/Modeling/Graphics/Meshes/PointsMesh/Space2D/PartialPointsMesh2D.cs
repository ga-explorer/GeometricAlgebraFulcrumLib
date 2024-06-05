using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsMesh.Space2D;

public sealed class PartialPointsMesh2D
    : PSeqPartial2D<ILinFloat64Vector2D>, IPointsMesh2D
{
    public IPointsMesh2D BaseMesh { get; }


    public PartialPointsMesh2D(IPointsMesh2D baseMesh)
        : base(baseMesh)
    {
        BaseMesh = baseMesh;
    }


    public override PSeqSlice1D<ILinFloat64Vector2D> GetSliceAt(int dimension, int index)
    {
        return new PointsMeshSlicePointsPath2D(this, dimension, index);
    }

    public PointsMeshSlicePointsPath2D GetSlicePathAt(int dimension, int index)
    {
        return new PointsMeshSlicePointsPath2D(this, dimension, index);
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}
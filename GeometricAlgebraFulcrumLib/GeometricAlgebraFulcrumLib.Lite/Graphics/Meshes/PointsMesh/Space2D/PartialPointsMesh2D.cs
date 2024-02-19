using DataStructuresLib.Sequences.Periodic1D;
using DataStructuresLib.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsMesh.Space2D;

public sealed class PartialPointsMesh2D
    : PSeqPartial2D<IFloat64Vector2D>, IPointsMesh2D
{
    public IPointsMesh2D BaseMesh { get; }


    public PartialPointsMesh2D(IPointsMesh2D baseMesh)
        : base(baseMesh)
    {
        BaseMesh = baseMesh;
    }


    public override PSeqSlice1D<IFloat64Vector2D> GetSliceAt(int dimension, int index)
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
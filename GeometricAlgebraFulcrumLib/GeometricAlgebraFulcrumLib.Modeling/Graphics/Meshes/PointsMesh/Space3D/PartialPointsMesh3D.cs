using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Permutations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsMesh.Space3D;

public sealed class PartialPointsMesh3D
    : PSeqPartial2D<ILinFloat64Vector3D>, IPointsMesh3D
{
    public IPointsMesh3D BaseMesh { get; }


    public PartialPointsMesh3D(IPointsMesh3D baseMesh, IndexMapRange1D range1, IndexMapRange1D range2)
        : base(baseMesh, range1, range2)
    {
        BaseMesh = baseMesh;
    }


    public override PSeqSlice1D<ILinFloat64Vector3D> GetSliceAt(int dimension, int index)
    {
        return new PointsMeshSlicePointsPath3D(this, dimension, index);
    }

    public PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index)
    {
        return new PointsMeshSlicePointsPath3D(this, dimension, index);
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}
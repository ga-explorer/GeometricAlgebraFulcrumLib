using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsMesh;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Permutations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class PointsMeshSubsetPointsPath3D : 
    PSeq2DSubset1D<ILinFloat64Vector3D>, 
    IPointsPath3D
{
    public IPointsMesh3D BaseMesh { get; }


    internal PointsMeshSubsetPointsPath3D(IPointsMesh3D baseMesh, IIndexMap1DTo2D indexMapping)
        : base(baseMesh, indexMapping)
    {
        BaseMesh = baseMesh;
    }

        
    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }

    public IPointsPath3D MapPoints(Func<ILinFloat64Vector3D, ILinFloat64Vector3D> pointMapping)
    {
        return new ArrayPointsPath3D(
            this.Select(pointMapping).ToArray()
        );
    }
}
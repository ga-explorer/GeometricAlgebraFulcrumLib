using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsMesh.Space3D;

public sealed class ComputedPointsMesh3D : 
    PSeqComputed2D<ILinFloat64Vector3D>, 
    IPointsMesh3D
{
    public ComputedPointsMesh3D(int count1, int count2, Func<int, int, ILinFloat64Vector3D> mappingFunc) 
        : base(count1, count2, mappingFunc)
    {
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
using DataStructuresLib.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsMesh.Space3D;

public sealed class ComputedPointsMesh3D : 
    PSeqComputed2D<IFloat64Vector3D>, 
    IPointsMesh3D
{
    public ComputedPointsMesh3D(int count1, int count2, Func<int, int, IFloat64Vector3D> mappingFunc) 
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
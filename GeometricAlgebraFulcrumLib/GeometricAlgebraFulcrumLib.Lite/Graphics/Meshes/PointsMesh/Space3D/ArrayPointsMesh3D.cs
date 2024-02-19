using DataStructuresLib.Sequences.Periodic1D;
using DataStructuresLib.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsMesh.Space3D;

public sealed class ArrayPointsMesh3D : 
    PSeqArray2D<IFloat64Vector3D>, 
    IPointsMesh3D
{
    public ArrayPointsMesh3D(int count1, int count2) 
        : base(count1, count2)
    {
        for (var i = 0; i < count1; i++)
        for (var j = 0; j < count2; j++)
            this[i, j] = Float64Vector3D.Zero;
    }

    public ArrayPointsMesh3D(IFloat64Vector3D[,] dataArray) 
        : base(dataArray)
    {
    }


    public override PSeqSlice1D<IFloat64Vector3D> GetSliceAt(int dimension, int index)
    {
        return new PointsMeshSlicePointsPath3D(this, dimension, index);
    }

    public PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index)
    {
        return new PointsMeshSlicePointsPath3D(this, dimension, index);
    }

    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }
}
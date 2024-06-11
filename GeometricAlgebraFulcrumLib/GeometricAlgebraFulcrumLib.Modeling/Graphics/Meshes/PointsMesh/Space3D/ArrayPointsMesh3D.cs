using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsMesh.Space3D;

public sealed class ArrayPointsMesh3D : 
    PSeqArray2D<ILinFloat64Vector3D>, 
    IPointsMesh3D
{
    public ArrayPointsMesh3D(int count1, int count2) 
        : base(count1, count2)
    {
        for (var i = 0; i < count1; i++)
        for (var j = 0; j < count2; j++)
            this[i, j] = LinFloat64Vector3D.Zero;
    }

    public ArrayPointsMesh3D(ILinFloat64Vector3D[,] dataArray) 
        : base(dataArray)
    {
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
        return this.All(p => p.IsValid());
    }
}
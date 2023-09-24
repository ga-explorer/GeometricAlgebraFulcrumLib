using DataStructuresLib.Sequences.Periodic1D;
using DataStructuresLib.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsMesh.Space2D
{
    public class ArrayPointsMesh2D
        : PSeqArray2D<IFloat64Vector2D>, IPointsMesh2D
    {
        public ArrayPointsMesh2D(int count1, int count2)
            : base(count1, count2)
        {
        }

        public ArrayPointsMesh2D(IFloat64Vector2D[,] dataArray)
            : base(dataArray)
        {
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
            return this.All(p => p.IsValid());
        }
    }
}
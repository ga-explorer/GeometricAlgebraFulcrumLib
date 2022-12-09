using DataStructuresLib.Sequences.Periodic1D;
using DataStructuresLib.Sequences.Periodic2D;
using NumericalGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D;

namespace GraphicsComposerLib.Geometry.Meshes.PointsMesh.Space2D
{
    public class ArrayPointsMesh2D
        : PSeqArray2D<IFloat64Tuple2D>, IPointsMesh2D
    {
        public ArrayPointsMesh2D(int count1, int count2)
            : base(count1, count2)
        {
        }

        public ArrayPointsMesh2D(IFloat64Tuple2D[,] dataArray)
            : base(dataArray)
        {
        }


        public override PSeqSlice1D<IFloat64Tuple2D> GetSliceAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath2D(this, dimension, index);
        }

        public PointsMeshSlicePointsPath2D GetSlicePathAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath2D(this, dimension, index);
        }
    }
}
using DataStructuresLib.Sequences.Periodic1D;
using DataStructuresLib.Sequences.Periodic2D;
using EuclideanGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Geometry.PointsPath.Space2D;

namespace GraphicsComposerLib.Geometry.Geometry.PointsMesh.Space2D
{
    public class ArrayPointsMesh2D
        : PSeqArray2D<ITuple2D>, IPointsMesh2D
    {
        public ArrayPointsMesh2D(int count1, int count2)
            : base(count1, count2)
        {
        }

        public ArrayPointsMesh2D(ITuple2D[,] dataArray)
            : base(dataArray)
        {
        }


        public override PSeqSlice1D<ITuple2D> GetSliceAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath2D(this, dimension, index);
        }

        public PointsMeshSlicePointsPath2D GetSlicePathAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath2D(this, dimension, index);
        }
    }
}
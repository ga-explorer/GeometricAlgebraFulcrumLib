using DataStructuresLib.Sequences.Periodic1D;
using DataStructuresLib.Sequences.Periodic2D;
using NumericalGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D;

namespace GraphicsComposerLib.Geometry.Meshes.PointsMesh.Space3D
{
    public sealed class ArrayPointsMesh3D : 
        PSeqArray2D<IFloat64Tuple3D>, 
        IPointsMesh3D
    {
        public ArrayPointsMesh3D(int count1, int count2) 
            : base(count1, count2)
        {
        }

        public ArrayPointsMesh3D(IFloat64Tuple3D[,] dataArray) 
            : base(dataArray)
        {
        }


        public override PSeqSlice1D<IFloat64Tuple3D> GetSliceAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath3D(this, dimension, index);
        }

        public PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath3D(this, dimension, index);
        }
    }
}
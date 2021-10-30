using DataStructuresLib.Permutations;
using DataStructuresLib.Sequences.Periodic1D;
using DataStructuresLib.Sequences.Periodic2D;
using EuclideanGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D;

namespace GraphicsComposerLib.Geometry.Geometry.PointsMesh.Space3D
{
    public sealed class PartialPointsMesh3D
        : PSeqPartial2D<ITuple3D>, IPointsMesh3D
    {
        public IPointsMesh3D BaseMesh { get; }


        public PartialPointsMesh3D(IPointsMesh3D baseMesh, IndexMapRange1D range1, IndexMapRange1D range2)
            : base(baseMesh, range1, range2)
        {
            BaseMesh = baseMesh;
        }


        public override PSeqSlice1D<ITuple3D> GetSliceAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath3D(this, dimension, index);
        }

        public PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath3D(this, dimension, index);
        }
    }
}
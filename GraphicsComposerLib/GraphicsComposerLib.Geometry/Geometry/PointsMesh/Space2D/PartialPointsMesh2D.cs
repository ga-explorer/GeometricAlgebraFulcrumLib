using DataStructuresLib.Sequences.Periodic1D;
using DataStructuresLib.Sequences.Periodic2D;
using EuclideanGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Geometry.PointsPath.Space2D;

namespace GraphicsComposerLib.Geometry.Geometry.PointsMesh.Space2D
{
    public sealed class PartialPointsMesh2D
        : PSeqPartial2D<ITuple2D>, IPointsMesh2D
    {
        public IPointsMesh2D BaseMesh { get; }


        public PartialPointsMesh2D(IPointsMesh2D baseMesh)
            : base(baseMesh)
        {
            BaseMesh = baseMesh;
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
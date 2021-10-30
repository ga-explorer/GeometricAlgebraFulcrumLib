using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Geometry.PointsMesh;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space2D
{
    public sealed class PointsMeshSlicePointsPath2D
        : PSeqSlice1D<ITuple2D>, IPointsPath2D
    {
        public IPointsMesh2D BaseMesh { get; }


        internal PointsMeshSlicePointsPath2D(IPointsMesh2D baseMesh, int sliceDimension, int sliceIndex)
            : base(baseMesh, sliceDimension, sliceIndex)
        {
            BaseMesh = baseMesh;
        }
    }
}
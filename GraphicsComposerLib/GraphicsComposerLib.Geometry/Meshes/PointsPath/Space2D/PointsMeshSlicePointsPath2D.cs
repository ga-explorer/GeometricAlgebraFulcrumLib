using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Meshes.PointsMesh;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    public sealed class PointsMeshSlicePointsPath2D
        : PSeqSlice1D<IFloat64Tuple2D>, IPointsPath2D
    {
        public IPointsMesh2D BaseMesh { get; }


        internal PointsMeshSlicePointsPath2D(IPointsMesh2D baseMesh, int sliceDimension, int sliceIndex)
            : base(baseMesh, sliceDimension, sliceIndex)
        {
            BaseMesh = baseMesh;
        }
    }
}
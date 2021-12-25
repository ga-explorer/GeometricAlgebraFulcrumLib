using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Meshes.PointsMesh;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public sealed class PointsMeshSlicePointsPath3D
        : PSeqSlice1D<ITuple3D>, IPointsPath3D
    {
        public IPointsMesh3D BaseMesh { get; }


        internal PointsMeshSlicePointsPath3D(IPointsMesh3D baseMesh, int sliceDimension, int sliceIndex) 
            : base(baseMesh, sliceDimension, sliceIndex)
        {
            BaseMesh = baseMesh;
        }
    }
}

using DataStructuresLib.Permutations;
using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Meshes.PointsMesh;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D
{
    public sealed class PointsMeshSubsetPointsPath3D
        : PSeq2DSubset1D<IFloat64Tuple3D>, IPointsPath3D
    {
        public IPointsMesh3D BaseMesh { get; }


        internal PointsMeshSubsetPointsPath3D(IPointsMesh3D baseMesh, IIndexMap1DTo2D indexMapping)
            : base(baseMesh, indexMapping)
        {
            BaseMesh = baseMesh;
        }
    }
}
using DataStructuresLib.Permutations;
using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Meshes.PointsMesh;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
    public sealed class PointsMeshSubsetPointsPath2D
        : PSeq2DSubset1D<IFloat64Tuple2D>, IPointsPath2D
    {
        public IPointsMesh2D BaseMesh { get; }


        internal PointsMeshSubsetPointsPath2D(IPointsMesh2D baseMesh, IIndexMap1DTo2D indexMapping)
            : base(baseMesh, indexMapping)
        {
            BaseMesh = baseMesh;
        }
    }
}
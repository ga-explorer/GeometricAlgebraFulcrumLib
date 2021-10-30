using DataStructuresLib.Permutations;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Geometry.PointsMesh;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space2D
{
    public sealed class PointsMeshSubsetPointsPath2D
        : PSeq2DSubset1D<ITuple2D>, IPointsPath2D
    {
        public IPointsMesh2D BaseMesh { get; }


        internal PointsMeshSubsetPointsPath2D(IPointsMesh2D baseMesh, IIndexMap1DTo2D indexMapping)
            : base(baseMesh, indexMapping)
        {
            BaseMesh = baseMesh;
        }
    }
}
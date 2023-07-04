using DataStructuresLib.Permutations;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsMesh;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space2D
{
    public sealed class PointsMeshSubsetPointsPath2D
        : PSeq2DSubset1D<IFloat64Vector2D>, IPointsPath2D
    {
        public IPointsMesh2D BaseMesh { get; }


        internal PointsMeshSubsetPointsPath2D(IPointsMesh2D baseMesh, IIndexMap1DTo2D indexMapping)
            : base(baseMesh, indexMapping)
        {
            BaseMesh = baseMesh;
        }

        
        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }

        public IPointsPath2D MapPoints(Func<IFloat64Vector2D, IFloat64Vector2D> pointMapping)
        {
            return new ArrayPointsPath2D(
                this.Select(pointMapping).ToArray()
            );
        }
    }
}
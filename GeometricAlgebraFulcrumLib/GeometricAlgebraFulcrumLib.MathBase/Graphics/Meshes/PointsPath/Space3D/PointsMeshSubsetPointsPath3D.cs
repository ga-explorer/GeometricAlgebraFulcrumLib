using DataStructuresLib.Permutations;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsMesh;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class PointsMeshSubsetPointsPath3D : 
        PSeq2DSubset1D<IFloat64Vector3D>, 
        IPointsPath3D
    {
        public IPointsMesh3D BaseMesh { get; }


        internal PointsMeshSubsetPointsPath3D(IPointsMesh3D baseMesh, IIndexMap1DTo2D indexMapping)
            : base(baseMesh, indexMapping)
        {
            BaseMesh = baseMesh;
        }

        
        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }

        public IPointsPath3D MapPoints(Func<IFloat64Vector3D, IFloat64Vector3D> pointMapping)
        {
            return new ArrayPointsPath3D(
                this.Select(pointMapping).ToArray()
            );
        }
    }
}
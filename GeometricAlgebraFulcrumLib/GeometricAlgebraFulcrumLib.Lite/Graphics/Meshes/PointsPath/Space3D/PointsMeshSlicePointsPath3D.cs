using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsMesh;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class PointsMeshSlicePointsPath3D : 
        PSeqSlice1D<IFloat64Vector3D>, 
        IPointsPath3D
    {
        public IPointsMesh3D BaseMesh { get; }


        internal PointsMeshSlicePointsPath3D(IPointsMesh3D baseMesh, int sliceDimension, int sliceIndex) 
            : base(baseMesh, sliceDimension, sliceIndex)
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

using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D
{
    public class ReversedPointsPath3D : 
        PSeqReverse1D<IFloat64Vector3D>, 
        IPointsPath3D
    {
        public IPointsPath3D BasePath { get; }


        public ReversedPointsPath3D(IPointsPath3D basePath)
            : base(basePath)
        {
            BasePath = basePath;
        }


        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }

        public IPointsPath3D MapPoints(Func<IFloat64Vector3D, IFloat64Vector3D> pointMapping)
        {
            return new ReversedPointsPath3D(
                BasePath.MapPoints(pointMapping)
            );
        }
    }
}
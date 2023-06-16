using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space2D
{
    public class ReversedPointsPath2D : 
        PSeqReverse1D<IFloat64Tuple2D>, 
        IPointsPath2D
    {
        public IPointsPath2D BasePath { get; }


        public ReversedPointsPath2D(IPointsPath2D basePath)
            : base(basePath)
        {
            BasePath = basePath;
        }

        
        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }

        public IPointsPath2D MapPoints(Func<IFloat64Tuple2D, IFloat64Tuple2D> pointMapping)
        {
            return new ReversedPointsPath2D(
                BasePath.MapPoints(pointMapping)
            );
        }
    }
}
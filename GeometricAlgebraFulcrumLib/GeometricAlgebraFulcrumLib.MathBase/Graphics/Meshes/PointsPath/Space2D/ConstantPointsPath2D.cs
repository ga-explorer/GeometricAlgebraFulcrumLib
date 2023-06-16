using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space2D
{
    public class ConstantPointsPath2D
        : PSeqConstant1D<IFloat64Tuple2D>, IPointsPath2D
    {
        public ConstantPointsPath2D(int count)
            : base(count, new Float64Vector2D(0, 0))
        {
        }

        public ConstantPointsPath2D(int count, double x, double y)
            : base(count, new Float64Vector2D(x, y))
        {
        }

        public ConstantPointsPath2D(int count, IFloat64Tuple2D value)
            : base(count, value)
        {
        }
        

        public bool IsValid()
        {
            return Count >= 2 && Value.IsValid();
        }

        public IPointsPath2D MapPoints(Func<IFloat64Tuple2D, IFloat64Tuple2D> pointMapping)
        {
            return new ConstantPointsPath2D(Count, pointMapping(Value));
        }
    }
}

using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space2D
{
    public class ConstantPointsPath2D
        : PSeqConstant1D<IFloat64Vector2D>, IPointsPath2D
    {
        public ConstantPointsPath2D(int count)
            : base(count, Float64Vector2D.Create((Float64Scalar)0, 0))
        {
        }

        public ConstantPointsPath2D(int count, double x, double y)
            : base(count, Float64Vector2D.Create((Float64Scalar)x, (Float64Scalar)y))
        {
        }

        public ConstantPointsPath2D(int count, IFloat64Vector2D value)
            : base(count, value)
        {
        }
        

        public bool IsValid()
        {
            return Count >= 2 && Value.IsValid();
        }

        public IPointsPath2D MapPoints(Func<IFloat64Vector2D, IFloat64Vector2D> pointMapping)
        {
            return new ConstantPointsPath2D(Count, pointMapping(Value));
        }
    }
}

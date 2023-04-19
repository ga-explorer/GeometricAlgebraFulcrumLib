using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Transforms
{
    public interface ITransform3D
    {
        Float64Tuple3D MapPoint(Float64Tuple3D point);
    }
}

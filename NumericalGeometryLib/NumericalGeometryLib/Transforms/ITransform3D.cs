using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.Transforms
{
    public interface ITransform3D
    {
        Float64Tuple3D MapPoint(Float64Tuple3D point);
    }
}

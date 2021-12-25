using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.Transforms
{
    public interface ITransform3D
    {
        Tuple3D MapPoint(Tuple3D point);
    }
}

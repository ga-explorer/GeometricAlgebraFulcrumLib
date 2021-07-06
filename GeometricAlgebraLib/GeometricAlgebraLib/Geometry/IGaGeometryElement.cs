using EuclideanGeometryLib.BasicMath;
using GeometricAlgebraLib.Processing.Scalars;

namespace GeometricAlgebraLib.Geometry
{
    public interface IGaGeometricElement<T> :
        IGeometricElement
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }
    }
}
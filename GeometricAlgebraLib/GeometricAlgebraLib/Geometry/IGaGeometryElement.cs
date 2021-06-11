using EuclideanGeometryLib.BasicMath;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Geometry
{
    public interface IGaGeometricElement<T> :
        IGeometricElement
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }
    }
}
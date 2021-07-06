using EuclideanGeometryLib.BasicMath;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public interface IGaGeometricElement<T> :
        IGeometricElement
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }
    }
}
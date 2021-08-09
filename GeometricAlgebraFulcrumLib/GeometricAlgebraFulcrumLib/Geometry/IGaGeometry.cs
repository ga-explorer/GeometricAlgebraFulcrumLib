using EuclideanGeometryLib.BasicMath;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public interface IGaGeometry<T> :
        IGeometricElement
    {
        uint VSpaceDimension { get; }

        ulong GaSpaceDimension { get; }

        IGaProcessor<T> Processor { get; }
    }
}
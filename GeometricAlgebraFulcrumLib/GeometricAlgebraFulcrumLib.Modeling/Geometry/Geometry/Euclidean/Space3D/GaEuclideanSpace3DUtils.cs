using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Geometry.Euclidean.Space3D;

public static class GaEuclideanSpace3DUtils
{
    public static RGaFloat64Processor GeometricProcessor { get; }
        = RGaFloat64Processor.Euclidean;
}
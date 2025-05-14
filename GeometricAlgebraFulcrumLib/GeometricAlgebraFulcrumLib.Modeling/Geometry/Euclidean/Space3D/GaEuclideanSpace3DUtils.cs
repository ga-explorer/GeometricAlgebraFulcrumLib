using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Euclidean.Space3D;

public static class GaEuclideanSpace3DUtils
{
    public static XGaFloat64Processor GeometricProcessor { get; }
        = XGaFloat64Processor.Euclidean;
}
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Float64;

/// <summary>
/// Projective Geometric Algebra for 3D Euclidean space
/// </summary>
public static class PGaEuc4D
{
    public static int VSpaceDimensions
        => 4;

    public static RGaFloat64ProjectiveProcessor Processor
        => RGaFloat64ProjectiveProcessor.Instance;

    public static RGaFloat64Vector E0 { get; }
        = Processor.VectorTerm(0);

    public static RGaFloat64Vector E1 { get; }
        = Processor.VectorTerm(1);

    public static RGaFloat64Vector E2 { get; }
        = Processor.VectorTerm(2);

    public static RGaFloat64Vector E3 { get; }
        = Processor.VectorTerm(3);

    public static RGaFloat64HigherKVector I { get; }
        = Processor.HigherKVectorTerm(
            new[] { 0, 1, 2, 3 }
        );

    public static RGaFloat64HigherKVector Irev { get; }
        = I.Reverse();


}
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Float64;

/// <summary>
/// Projective Geometric Algebra for 3D Euclidean space
/// </summary>
public static class PGaEuc4D
{
    public static int VSpaceDimensions
        => 4;

    public static XGaFloat64ProjectiveProcessor Processor
        => XGaFloat64ProjectiveProcessor.Instance;

    public static XGaFloat64Vector E0 { get; }
        = Processor.VectorTerm(0);

    public static XGaFloat64Vector E1 { get; }
        = Processor.VectorTerm(1);

    public static XGaFloat64Vector E2 { get; }
        = Processor.VectorTerm(2);

    public static XGaFloat64Vector E3 { get; }
        = Processor.VectorTerm(3);

    public static XGaFloat64HigherKVector I { get; }
        = Processor.HigherKVectorTerm(
            new[] { 0, 1, 2, 3 }.ToIndexSet(true)
        );

    public static XGaFloat64HigherKVector Irev { get; }
        = I.Reverse();


}
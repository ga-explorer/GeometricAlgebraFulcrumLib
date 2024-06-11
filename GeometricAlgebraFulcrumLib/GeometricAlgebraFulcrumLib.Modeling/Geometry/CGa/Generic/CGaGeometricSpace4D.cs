using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic;

/// <summary>
/// Conformal Geometric Algebra for 2D Euclidean Space
/// </summary>
public sealed class CGaGeometricSpace4D<T> :
    CGaGeometricSpace<T>
{
    public static CGaGeometricSpace4D<T> Create(IScalarProcessor<T> scalarProcessor)
        => new CGaGeometricSpace4D<T>(scalarProcessor);


    private CGaGeometricSpace4D(IScalarProcessor<T> scalarProcessor)
        : base(scalarProcessor, 4)
    {
    }
}
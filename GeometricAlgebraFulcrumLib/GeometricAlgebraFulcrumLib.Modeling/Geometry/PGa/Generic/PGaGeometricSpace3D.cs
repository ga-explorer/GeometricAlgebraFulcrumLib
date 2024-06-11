using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic;

/// <summary>
/// Projective Geometric Algebra for 2D Euclidean Space
/// </summary>
public sealed class PGaGeometricSpace3D<T> :
    PGaGeometricSpace<T>
{
    public static PGaGeometricSpace3D<T> Create(IScalarProcessor<T> scalarProcessor)
        => new PGaGeometricSpace3D<T>(scalarProcessor);


    private PGaGeometricSpace3D(IScalarProcessor<T> scalarProcessor)
        : base(scalarProcessor, 3)
    {
    }
}
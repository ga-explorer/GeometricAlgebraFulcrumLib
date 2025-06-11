using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic;

/// <summary>
/// 5D Projective Geometric Algebra for 3D Euclidean Space
/// </summary>
public sealed class PGaGeometricSpace4D<T> :
    PGaGeometricSpace<T>
{
    public static PGaGeometricSpace4D<T> Create(IScalarProcessor<T> scalarProcessor)
        => new PGaGeometricSpace4D<T>(scalarProcessor);


    public PGaBlade<T> E3 { get; }

    public PGaBlade<T> E13 { get; }

    public PGaBlade<T> E23 { get; }


    private PGaGeometricSpace4D(IScalarProcessor<T> scalarProcessor)
        : base(scalarProcessor, 4)
    {
        E3 = new PGaBlade<T>(this, ProjectiveProcessor.VectorTerm(3));

        E13 = new PGaBlade<T>(this, ProjectiveProcessor.BivectorTerm(1, 3));
        E23 = new PGaBlade<T>(this, ProjectiveProcessor.BivectorTerm(2, 3));
    }
}
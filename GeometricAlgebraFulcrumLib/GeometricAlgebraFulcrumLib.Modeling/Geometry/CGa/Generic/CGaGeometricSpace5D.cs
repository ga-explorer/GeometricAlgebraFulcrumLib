using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic;

/// <summary>
/// 5D Conformal Geometric Algebra for 3D Euclidean Space
/// </summary>
public sealed class CGaGeometricSpace5D<T> :
    CGaGeometricSpace<T>
{
    public static CGaGeometricSpace5D<T> Create(IScalarProcessor<T> scalarProcessor)
        => new CGaGeometricSpace5D<T>(scalarProcessor);


    public CGaBlade<T> E3 { get; }

    public CGaBlade<T> E13 { get; }

    public CGaBlade<T> E23 { get; }


    private CGaGeometricSpace5D(IScalarProcessor<T> scalarProcessor)
        : base(scalarProcessor, 5)
    {
        E3 = new CGaBlade<T>(this, ConformalProcessor.VectorTerm(4));

        E13 = new CGaBlade<T>(this, ConformalProcessor.BivectorTerm(2, 4));
        E23 = new CGaBlade<T>(this, ConformalProcessor.BivectorTerm(3, 4));
    }
}
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal;

/// <summary>
/// 5D Conformal Geometric Algebra for 3D Euclidean Space
/// </summary>
public sealed class XGaConformalSpace5D<T> :
    XGaConformalSpace<T>
{
    public static XGaConformalSpace5D<T> Create(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalSpace5D<T>(scalarProcessor);


    public XGaConformalBlade<T> E3 { get; }

    public XGaConformalBlade<T> E13 { get; }

    public XGaConformalBlade<T> E23 { get; }


    private XGaConformalSpace5D(IScalarProcessor<T> scalarProcessor) 
        : base(scalarProcessor, 5)
    {
        E3 = new XGaConformalBlade<T>(this, ConformalProcessor.VectorTerm(4));

        E13 = new XGaConformalBlade<T>(this, ConformalProcessor.BivectorTerm(2, 4));
        E23 = new XGaConformalBlade<T>(this, ConformalProcessor.BivectorTerm(3, 4));
    }
}
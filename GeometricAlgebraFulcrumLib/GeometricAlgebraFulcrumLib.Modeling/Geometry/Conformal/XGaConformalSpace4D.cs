using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal;

/// <summary>
/// Conformal Geometric Algebra for 2D Euclidean Space
/// </summary>
public sealed class XGaConformalSpace4D<T> :
    XGaConformalSpace<T>
{
    public static XGaConformalSpace4D<T> Create(IScalarProcessor<T> scalarProcessor)
        => new XGaConformalSpace4D<T>(scalarProcessor);
    

    private XGaConformalSpace4D(IScalarProcessor<T> scalarProcessor) 
        : base(scalarProcessor, 4)
    {
    }
}
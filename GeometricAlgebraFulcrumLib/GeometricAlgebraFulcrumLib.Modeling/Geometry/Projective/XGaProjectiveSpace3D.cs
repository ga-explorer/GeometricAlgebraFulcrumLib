using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective;

/// <summary>
/// Projective Geometric Algebra for 2D Euclidean Space
/// </summary>
public sealed class XGaProjectiveSpace3D<T> :
    XGaProjectiveSpace<T>
{
    public static XGaProjectiveSpace3D<T> Create(IScalarProcessor<T> scalarProcessor)
        => new XGaProjectiveSpace3D<T>(scalarProcessor);
    

    private XGaProjectiveSpace3D(IScalarProcessor<T> scalarProcessor) 
        : base(scalarProcessor, 3)
    {
    }
}
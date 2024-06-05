using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective;

/// <summary>
/// 5D Projective Geometric Algebra for 3D Euclidean Space
/// </summary>
public sealed class XGaProjectiveSpace4D<T> :
    XGaProjectiveSpace<T>
{
    public static XGaProjectiveSpace4D<T> Create(IScalarProcessor<T> scalarProcessor)
        => new XGaProjectiveSpace4D<T>(scalarProcessor);


    public XGaProjectiveBlade<T> E3 { get; }

    public XGaProjectiveBlade<T> E13 { get; }

    public XGaProjectiveBlade<T> E23 { get; }


    private XGaProjectiveSpace4D(IScalarProcessor<T> scalarProcessor) 
        : base(scalarProcessor, 4)
    {
        E3 = new XGaProjectiveBlade<T>(this, ProjectiveProcessor.VectorTerm(3));

        E13 = new XGaProjectiveBlade<T>(this, ProjectiveProcessor.BivectorTerm(1, 3));
        E23 = new XGaProjectiveBlade<T>(this, ProjectiveProcessor.BivectorTerm(2, 3));
    }
}
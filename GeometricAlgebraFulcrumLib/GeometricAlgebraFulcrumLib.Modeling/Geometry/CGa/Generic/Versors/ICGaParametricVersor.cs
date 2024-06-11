using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Versors;

/// <summary>
/// A parametric conformal versor
/// </summary>
public interface ICGaParametricVersor<T> :
    IAlgebraicElement
{
    CGaGeometricSpace<T> ConformalSpace { get; }

    ScalarRange<T> ParameterRange { get; }

    CGaVersor<T> GetVersor(Scalar<T> parameterValue);
}
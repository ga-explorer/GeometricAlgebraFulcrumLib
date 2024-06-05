using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Versors;

/// <summary>
/// A parametric conformal versor
/// </summary>
public interface IXGaConformalParametricVersor<T> :
    IAlgebraicElement
{
    XGaConformalSpace<T> ConformalSpace { get; }

    ScalarRange<T> ParameterRange { get; }

    XGaConformalVersor<T> GetVersor(Scalar<T> parameterValue);
}
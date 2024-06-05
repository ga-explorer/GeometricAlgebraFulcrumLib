using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Versors;

/// <summary>
/// A parametric conformal versor
/// </summary>
public interface IRGaConformalParametricVersor :
    IAlgebraicElement
{
    RGaConformalSpace ConformalSpace { get; }

    Float64ScalarRange ParameterRange { get; }

    RGaConformalVersor GetVersor(double parameterValue);
}
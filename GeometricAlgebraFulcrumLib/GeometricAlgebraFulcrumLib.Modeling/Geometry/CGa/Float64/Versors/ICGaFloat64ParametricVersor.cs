using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Versors;

/// <summary>
/// A parametric conformal versor
/// </summary>
public interface ICGaFloat64ParametricVersor :
    IAlgebraicElement
{
    CGaFloat64GeometricSpace ConformalSpace { get; }

    Float64ScalarRange ParameterRange { get; }

    CGaFloat64Versor GetVersor(double parameterValue);
}
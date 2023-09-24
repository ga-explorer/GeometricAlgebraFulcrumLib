using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Versors;

/// <summary>
/// A parametric conformal versor
/// </summary>
public interface IRGaConformalParametricVersor :
    IGeometricElement
{
    RGaConformalSpace ConformalSpace { get; }

    Float64ScalarRange ParameterRange { get; }

    RGaConformalVersor GetVersor(double parameterValue);
}